using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ScalableTeams.HumanResourcesManagement.API.Endpoints;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this WebApplication builder)
    {
        IServiceScope scope = builder.Services.CreateScope();

        IEnumerable<IEndpoint> endpoints = scope.ServiceProvider.GetServices<IEndpoint>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.AddRoute(builder);
        }
    }
}
