using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Common;
using ScalableTeams.HumanResourcesManagement.Application.Common;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using System;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.ManagersEndpoints;

public class GetAllVacationsRequestsEndpoint : IEndpoint
{
    private readonly IValidator<GetAllActiveVacationsRequestInput> validator;

    public GetAllVacationsRequestsEndpoint(IValidator<GetAllActiveVacationsRequestInput> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/managers/{managerId}/pending-reviews/vacations/",
            async (
                [FromRoute] Guid managerId,
                [FromServices] IFeatureService<GetAllActiveVacationsRequestInput, GetAllActiveVacationsRequestResult> service,
                CancellationToken cancellationToken) =>
        {
            var input = new GetAllActiveVacationsRequestInput
            {
                ManagerId = managerId
            };

            validator.ValidateAndThrow(input);

            var result = await service.Execute(
                input,
                cancellationToken);

            return Results.Ok(result);
        })
        .Produces<GetAllActiveVacationsRequestResult>()
        .WithTags("ManagersEndpoints");
    }
}