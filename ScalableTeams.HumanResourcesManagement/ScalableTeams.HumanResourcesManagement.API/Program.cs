using System;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Hubs;
using ScalableTeams.HumanResourcesManagement.API.Middlewares;
using ScalableTeams.HumanResourcesManagement.API.Security;

namespace ScalableTeams.HumanResourcesManagement.API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder
                    .AddConsole()
                    .AddDebug()
                    .SetMinimumLevel(LogLevel.Debug));

        // Add services to the container.
        ConfigurationManager configuration = builder.Configuration;
        IServiceCollection services = builder.Services;

        services
            .AddServices(configuration)
            .AddAuthorization()
            .AddEndpoints()
            .AddFeatureServices()
            .AddRepositories()
            .AddNotificationsServices()
            .AddTokenAuthentication(configuration)
            .AddDatabase(configuration)
            .AddEventDomains()
            .AddAccountingService(configuration)
            .AddAuthentication();

        builder.Services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
        builder.Services.AddSignalR();

        builder
            .Services
            .AddAuthorizationBuilder()
            .AddPolicy(SecurityPolicies.ManagersPolicy, policy => policy.RequireRole(SecurityRoles.ManagerRole))
            .AddPolicy(SecurityPolicies.HumanResourcesPolicy, policy => policy.RequireRole(SecurityRoles.HumanResourcesRole));

        builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.ConfigureHttpJsonOptions(opts =>
        {
            opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseRouting();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionHandler>();

        app
            .MapHub<ManagersHub>("/managers")
            .RequireAuthorization(SecurityPolicies.ManagersPolicy);

        app
            .MapHub<HumanResourcesHub>("/hr")
            .RequireAuthorization(SecurityPolicies.HumanResourcesPolicy);

        app
            .MapHub<HumanResourcesHub>("/employees")
            .RequireAuthorization();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapEndpoints();

        app.Run();
    }
}
