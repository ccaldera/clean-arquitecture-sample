using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;
using System;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.HumanResourcesEndpoints;

public class HrReviewRequestEndpoint : IEndpoint
{
    private readonly IValidator<HumanResourcesReviewRequestsInput> validator;

    public HrReviewRequestEndpoint(IValidator<HumanResourcesReviewRequestsInput> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/hr/pending-reviews/vacations/{vacationsRequestId}",
            async (
                HttpContext httpContext,
                [FromRoute] Guid vacationsRequestId,
                [FromServices] IFeatureService<HumanResourcesReviewRequestsInput> service,
                [FromBody] VactionRequestsStatus status,
                CancellationToken cancellationToken) =>
            {
                var input = new HumanResourcesReviewRequestsInput
                {
                    HrEmployeeId = httpContext.GetUserId(),
                    VacationRequestId = vacationsRequestId,
                    NewStatus = status,
                };

                validator.ValidateAndThrow(input);

                await service.Execute(input, cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization(SecurityPolicies.HumanResourcesPolicy)
            .WithTags("Human Resources Endpoints");
    }
}