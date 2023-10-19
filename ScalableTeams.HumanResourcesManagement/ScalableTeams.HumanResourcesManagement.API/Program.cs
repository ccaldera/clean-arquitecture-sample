using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Middlewares;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Persistence;
using ScalableTeams.HumanResourcesManagement.Persistence.Repositories;
using System;

namespace ScalableTeams.HumanResourcesManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddEndpoints();
            builder.Services.AddFeatureServices();
            builder.Services.AddRepositories();

            var connectionString = builder.Configuration.GetConnectionString("HumanResourcesManagementConnectionString")!;

            builder.Services.AddDbContext<HumanResourcesManagementContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapEndpoints();

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandler>();

            app.UseAuthorization();

            app.Run();
        }
    }
}