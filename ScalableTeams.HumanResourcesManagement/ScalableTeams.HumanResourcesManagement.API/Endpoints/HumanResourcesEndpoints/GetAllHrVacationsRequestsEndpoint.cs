using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.Application.Features;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
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
        .WithTags("Human Resources Endpoints");
    }
}
