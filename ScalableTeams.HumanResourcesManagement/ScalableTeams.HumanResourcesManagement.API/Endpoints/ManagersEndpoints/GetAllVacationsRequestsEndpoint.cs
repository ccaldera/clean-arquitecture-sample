using System.Threading;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints;

public class GetAllVacationsRequestsEndpoint : IEndpoint
{
    private readonly IValidator<GetOpenVacationsRequestsInput> validator;

    public GetAllVacationsRequestsEndpoint(IValidator<GetOpenVacationsRequestsInput> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/managers/pending-reviews/vacations/",
            async (
                HttpContext httpContext,
                [FromServices] IFeatureService<GetOpenVacationsRequestsInput, GetOpenVacationsRequestsResult> service,
                CancellationToken cancellationToken) =>
        {
            var input = new GetOpenVacationsRequestsInput
            {
                ManagerId = httpContext.GetUserId()
            };

            validator.ValidateAndThrow(input);

            var result = await service.Execute(
                input,
                cancellationToken);

            return Results.Ok(result);
        })
        .Produces<GetOpenVacationsRequestsResult>()
        .RequireAuthorization(SecurityPolicies.ManagersPolicy)
        .WithTags("Managers Endpoints");
    }
}