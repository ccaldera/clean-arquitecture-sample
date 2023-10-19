using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
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
        app.MapPost("api/hr/pending-reviews/vacations/{vacationsRequestId}",
            async (
                HttpContext httpContext,
                [FromRoute] Guid vacationsRequestId,
                [FromServices] IFeatureService<HrReviewRequest> service,
                [FromBody] ProcessStatus status,
                CancellationToken cancellationToken) =>
            {
                var input = new HrReviewRequest
                {
                    HrEmployeeId = httpContext.GetUserId(),
                    VacationRequestId = vacationsRequestId,
                    NewStatus = status,
                };

                validator.ValidateAndThrow(input);

                await service.Execute(input, cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization("Human Resources")
            .WithTags("Human Resources Endpoints");
    }
}