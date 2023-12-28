using System.Threading;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Endpoints;
using ScalableTeams.HumanResourcesManagement.API.Security.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;

namespace ScalableTeams.HumanResourcesManagement.API.Security;

public class GetTokenEndpoint : IEndpoint
{
    private readonly IValidator<GetTokenRequest> validator;

    public GetTokenEndpoint(IValidator<GetTokenRequest> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/get-token",
            async (
                [FromServices] IFeatureService<GetTokenRequest, GetTokenResponse?> service,
                [FromBody] GetTokenRequest request,
                CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                var response = await service.Execute(request, cancellationToken);

                if (response is null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);
            })
            .AllowAnonymous()
            .Produces<GetTokenResponse>()
            .WithTags("Security");
    }
}