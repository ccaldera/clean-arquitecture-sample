using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Models;
using ScalableTeams.HumanResourcesManagement.Application.Features;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.Security;

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

                if(response is null)
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