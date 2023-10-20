using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ScalableTeams.HumanResourcesManagement.API.Configuration;
using ScalableTeams.HumanResourcesManagement.API.Interfaces;
using ScalableTeams.HumanResourcesManagement.API.Security.Services;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Persistence;
using ScalableTeams.HumanResourcesManagement.Persistence.Repositories;
using System;
using System.Linq;
using System.Text;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class IServiceCollectionExtensions
{
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

    public static IServiceCollection AddHubs(this IServiceCollection services)
    {
        var endpoints = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(IHub)))
            .Where(t => !t.IsInterface);

        foreach (var endpoint in endpoints)
        {
            services.AddScoped(typeof(IHub), endpoint);
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
            .Where(t => !t.IsInterface);

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
}
