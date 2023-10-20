using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Middlewares;
using ScalableTeams.HumanResourcesManagement.API.Security;
using System;
using System.Text.Json.Serialization;

namespace ScalableTeams.HumanResourcesManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var configuration = builder.Configuration;

            builder.Services
                .AddServices(configuration)
                .AddAuthorization()
                .AddEndpoints()
                .AddFeatureServices()
                .AddRepositories()
                .AddHubs()
                .AddNotificationsServices()
                .AddTokenAuthentication(configuration)
                .AddDatabase(configuration)
                .AddAuthentication();

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
            builder.Services.AddSwaggerGen(opt =>
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
                }); ;
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHubsEndpoints();

            app.MapEndpoints();

            app.Run();
        }
    }
}