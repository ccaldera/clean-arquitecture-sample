using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Interfaces;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using System.Threading;

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
                [FromServices] IFeatureService<GetAllHrActiveVacationsRequestInput, GetAllHrActiveVacationsRequestResult> service,
                CancellationToken cancellationToken) =>
            {
                var result = await service.Execute(
                    new GetAllHrActiveVacationsRequestInput(),
                    cancellationToken);

                return Results.Ok(result);
            })
        .Produces<GetAllHrActiveVacationsRequestResult>()
        .RequireAuthorization(SecurityPolicies.HumanResourcesPolicy)
        .WithTags("Human Resources Endpoints");
    }
}
