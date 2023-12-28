using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.HumanResourcesEndpoints;

public class GetAllHrVacationsRequestsEndpoint : IEndpoint
{
    public GetAllHrVacationsRequestsEndpoint()
    {
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/hr/pending-reviews/vacations/",
            async (
                [FromServices] IFeatureService<GetOpenVacationRequestsInput, GetOpenVacationRequestsResult> service,
                CancellationToken cancellationToken) =>
            {
                GetOpenVacationRequestsResult result = await service.Execute(
                    new GetOpenVacationRequestsInput(),
                    cancellationToken);

                return Results.Ok(result);
            })
        .Produces<GetOpenVacationRequestsResult>()
        .RequireAuthorization(SecurityPolicies.HumanResourcesPolicy)
        .WithTags("Human Resources Endpoints");
    }
}
