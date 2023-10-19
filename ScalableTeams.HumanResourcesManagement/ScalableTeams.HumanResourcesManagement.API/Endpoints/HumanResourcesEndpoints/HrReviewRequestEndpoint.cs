using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.Application.Features;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using System;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.HumanResourcesEndpoints;

public class HrReviewRequestEndpoint : IEndpoint
{
    private readonly IValidator<HrReviewRequest> validator;

    public HrReviewRequestEndpoint(IValidator<HrReviewRequest> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/hr/{hrEmployeeId}/pending-reviews/vacations/{vacationsRequestId}",
            async (
                [FromRoute] Guid hrEmployeeId,
                [FromRoute] Guid vacationsRequestId,
                [FromServices] IFeatureService<HrReviewRequest> service,
                [FromBody] ProcessStatus status,
                CancellationToken cancellationToken) =>
            {
                var input = new HrReviewRequest
                {
                    HrEmployeeId = hrEmployeeId,
                    VacationRequestId = vacationsRequestId,
                    NewStatus = status,
                };

                validator.ValidateAndThrow(input);

                await service.Execute(input, cancellationToken);

                return Results.Ok();
            })
        .WithTags("Human Resources Endpoints");
    }
}