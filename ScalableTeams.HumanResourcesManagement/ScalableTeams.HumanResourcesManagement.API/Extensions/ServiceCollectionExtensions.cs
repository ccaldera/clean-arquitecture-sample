using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScalableTeams.HumanResourcesManagement.API.Configuration;
using ScalableTeams.HumanResourcesManagement.API.Endpoints;
using ScalableTeams.HumanResourcesManagement.API.Security.Services;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Infrastucture.Services;
using ScalableTeams.HumanResourcesManagement.Persistence;
using ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Scalable Teams HR", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });
    }

    public static IServiceCollection AddAccountingService(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHttpClient(nameof(AccountingService), c =>
        {
            var baseUrl = configuration.GetSection("AccountingServiceConfiguration").GetSection("BaseUrl").Value!;
            c.BaseAddress = new Uri(baseUrl);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped<IAccountingService, AccountingService>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

        return services
            .AddSingleton<IJwtService, JwtService>();
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("HumanResourcesManagementConnectionString")!;

        services.AddDbContext<HumanResourcesManagementContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var secret = configuration.GetSection("Jwt").GetSection("Secret").Value!;
        var key = Encoding.ASCII.GetBytes(secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        return services;
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var endpoints = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(IEndpoint)))
            .Where(t => !t.IsInterface);

        foreach (var endpoint in endpoints)
        {
            services.AddScoped(typeof(IEndpoint), endpoint);
        }

        return services;
    }

    public static IServiceCollection AddFeatureServices(this IServiceCollection services)
    {
        var types = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(x => !x.IsInterface);

        var inputOutputRequest = types
            .Where(x => x.GetInterfaces().Any(x => x.IsGenericType && typeof(IFeatureService<,>) == x.GetGenericTypeDefinition()));

        foreach (var feature in inputOutputRequest)
        {
            var @interface = feature
                .GetInterfaces()
                .First(x => x.IsGenericType && typeof(IFeatureService<,>) == x.GetGenericTypeDefinition());

            services.AddScoped(@interface, feature);
        }

        var inputOnlyRequest = types
            .Where(x => x.GetInterfaces().Any(x => x.IsGenericType && typeof(IFeatureService<>) == x.GetGenericTypeDefinition()));

        foreach (var feature in inputOnlyRequest)
        {
            var @interface = feature
                .GetInterfaces()
                .First(x => x.IsGenericType && typeof(IFeatureService<>) == x.GetGenericTypeDefinition());

            services.AddScoped(@interface, feature);
        }

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var repositories = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(IRepository)))
            .Where(t => !t.IsInterface && !t.IsAbstract);

        foreach (var repository in repositories)
        {
            var @interface = repository
                .GetInterfaces()
                .First(x => x != typeof(IRepository) && x.IsAssignableTo(typeof(IRepository)));

            services.AddScoped(@interface, repository);
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddNotificationsServices(this IServiceCollection services)
    {
        var repositories = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(INotificationService)))
            .Where(t => !t.IsInterface);

        foreach (var repository in repositories)
        {
            var @interface = repository
                .GetInterfaces()
                .First(x => x != typeof(INotificationService) && x.IsAssignableTo(typeof(INotificationService)));

            services.AddSingleton(@interface, repository);
        }
        return services;
    }

    public static IServiceCollection AddEventDomains(this IServiceCollection services)
    {
        var domainEvents = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(x => x.GetInterfaces().Any(x =>
                x.IsGenericType &&
                typeof(IDomainEventHandler<>) == x.GetGenericTypeDefinition()))
            .Where(x => !x.IsAbstract);

        foreach (var domainEvent in domainEvents)
        {
            var @interface = domainEvent
                .GetInterfaces()
                .First(x => x.IsGenericType && typeof(IDomainEventHandler<>) == x.GetGenericTypeDefinition());

            services.AddScoped(@interface, domainEvent);
        }

        services.AddScoped<IEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
