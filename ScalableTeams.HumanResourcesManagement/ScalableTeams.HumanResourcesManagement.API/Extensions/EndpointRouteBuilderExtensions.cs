﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ScalableTeams.HumanResourcesManagement.API.Endpoints;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class EndpointRouteBuilderExtensions
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
}