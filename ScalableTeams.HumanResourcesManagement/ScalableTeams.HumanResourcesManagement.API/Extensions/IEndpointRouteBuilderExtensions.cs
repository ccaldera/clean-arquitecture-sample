using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ScalableTeams.HumanResourcesManagement.API.Interfaces;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class IEndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this WebApplication builder)
    {
        var scope = builder.Services.CreateScope();

        var endpoints = scope.ServiceProvider.GetServices<IEndpoint>();

        foreach (var endpoint in endpoints)
        {
            endpoint.AddRoute(builder);
        }
    }

    public static void MapHubsEndpoints(this WebApplication builder)
    {
        var scope = builder.Services.CreateScope();

        var hubEndpoints = scope.ServiceProvider.GetServices<IHub>();

        foreach (var hubEndpoint in hubEndpoints)
        {
            hubEndpoint.AddHub(builder);
        }
    }
}