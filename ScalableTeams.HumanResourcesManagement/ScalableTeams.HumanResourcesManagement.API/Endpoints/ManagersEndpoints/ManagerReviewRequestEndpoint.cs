using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.API.Security;
using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResources.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using System;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints;

public class ManagerReviewRequestEndpoint : IEndpoint
{
    private readonly IValidator<ManagerReviewRequest> validator;

    public ManagerReviewRequestEndpoint(IValidator<ManagerReviewRequest> validator)
    {
        this.validator = validator;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/managers/pending-reviews/vacations/{vacationsRequestId}",
            async (
                HttpContext httpContext,
                [FromRoute] Guid vacationsRequestId,
                [FromServices] IFeatureService<ManagerReviewRequest> service,
                [FromBody] ProcessStatus status,
                CancellationToken cancellationToken) =>
            {
                var input = new ManagerReviewRequest
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