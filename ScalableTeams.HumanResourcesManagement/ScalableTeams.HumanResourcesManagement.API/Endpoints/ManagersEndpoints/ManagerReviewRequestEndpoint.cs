using System;
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
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints;

public class ManagerReviewRequestEndpoint : IEndpoint
{
    private readonly IValidator<ManagerReviewRequestInput> validator;

    public ManagerReviewRequestEndpoint(IValidator<ManagerReviewRequestInput> validator)
    {
        this.validator = validator;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/managers/pending-reviews/vacations/{vacationsRequestId}",
            async (
                HttpContext httpContext,
                [FromRoute] Guid vacationsRequestId,
                [FromServices] IFeatureService<ManagerReviewRequestInput> service,
                [FromBody] VactionRequestsStatus status,
                CancellationToken cancellationToken) =>
            {
                var input = new ManagerReviewRequestInput
                {
                    ReviewerId = httpContext.GetUserId(),
                    VacationRequestId = vacationsRequestId,
                    NewStatus = status,
                };

                validator.ValidateAndThrow(input);

                await service.Execute(input, cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization(SecurityPolicies.ManagersPolicy)
            .WithTags("Managers Endpoints");
    }
}