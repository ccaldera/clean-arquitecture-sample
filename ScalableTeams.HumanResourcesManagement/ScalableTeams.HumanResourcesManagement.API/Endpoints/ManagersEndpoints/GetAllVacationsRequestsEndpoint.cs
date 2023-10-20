using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints;

public class GetAllVacationsRequestsEndpoint : IEndpoint
{
    private readonly IValidator<GetAllActiveVacationsRequestInput> validator;

    public GetAllVacationsRequestsEndpoint(IValidator<GetAllActiveVacationsRequestInput> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/managers/pending-reviews/vacations/",
            async (
                HttpContext httpContext,
                [FromServices] IFeatureService<GetAllActiveVacationsRequestInput, GetAllActiveVacationsRequestResult> service,
                CancellationToken cancellationToken) =>
        {
            var input = new GetAllActiveVacationsRequestInput
            {
                ManagerId = httpContext.GetUserId()
            };

            validator.ValidateAndThrow(input);

            var result = await service.Execute(
                input,
                cancellationToken);

            return Results.Ok(result);
        })
        .Produces<GetAllActiveVacationsRequestResult>()
        .RequireAuthorization(SecurityPolicies.ManagersPolicy)
        .WithTags("Managers Endpoints");
    }
}