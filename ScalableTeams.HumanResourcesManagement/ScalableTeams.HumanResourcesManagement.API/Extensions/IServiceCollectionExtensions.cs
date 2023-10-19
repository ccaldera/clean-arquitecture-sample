using Microsoft.Extensions.DependencyInjection;
using ScalableTeams.HumanResourcesManagement.API.Endpoints;
using ScalableTeams.HumanResourcesManagement.Application.Features;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Persistence.Repositories;
using System;
using System.Linq;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class IServiceCollectionExtensions
{
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
}
