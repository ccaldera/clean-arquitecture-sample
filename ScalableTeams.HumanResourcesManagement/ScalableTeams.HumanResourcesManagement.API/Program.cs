using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScalableTeams.HumanResourcesManagement.API.DomainEvents;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Hubs;
using ScalableTeams.HumanResourcesManagement.API.Middlewares;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.EmployeeRequestsVacations;
using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests;
using ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequestRejected;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;
using System;
using System.Text.Json.Serialization;

namespace ScalableTeams.HumanResourcesManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var loggerFactory = LoggerFactory.Create(builder => builder
                        .AddConsole()
                        .AddDebug()
                        .SetMinimumLevel(LogLevel.Debug));

            // Add services to the container.
            var configuration = builder.Configuration;
            var services = builder.Services;

            services
                .AddServices(configuration)
                .AddAuthorization()
                .AddEndpoints()
                .AddFeatureServices()
                .AddRepositories()
                .AddNotificationsServices()
                .AddTokenAuthentication(configuration)
                .AddDatabase(configuration)
                .AddAccountingService(configuration)
                .AddAuthentication();

            services.AddScoped<IEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IDomainEventHandler<VacationRequestCreated>, NotifyManagementHandler>();
            services.AddScoped<IDomainEventHandler<VacationRequestApprovedByManager>, NotifyHumanResourcesHandler>();
            services.AddScoped<IDomainEventHandler<VacationRequestApprovedByManager>, Application.Features.ManagerReviewOpenVacationRequests.NotifyEmployeeHandler>();
            services.AddScoped<IDomainEventHandler<VacationRequestApprovedByHumanResources>, NotifyAccountService>();
            services.AddScoped<IDomainEventHandler<VacationRequestApprovedByHumanResources>, Application.Features.HumanResourcesReviewOpenRequests.NotifyEmployeeHandler>();
            services.AddScoped<IDomainEventHandler<VacationRequestRejected>, VacationsRequestRejectedHandler>();

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

            var app = builder.Build();

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
}