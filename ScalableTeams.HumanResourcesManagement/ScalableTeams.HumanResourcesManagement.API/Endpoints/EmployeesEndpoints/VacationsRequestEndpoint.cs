using System;
using System.Collections.Generic;
using System.Threading;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Extensions;
using ScalableTeams.HumanResourcesManagement.Application.Features.EmployeeRequestsVacations.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.EmployeesEndpoints;

public class VacationsRequestEndpoint : IEndpoint
{
    private readonly IValidator<VacationsRequestInput> validator;

    public VacationsRequestEndpoint(IValidator<VacationsRequestInput> validator)
    {
        this.validator = validator;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employees/requests/vacations",
            async (
                HttpContext httpContext,
                [FromBody] List<DateTime> dates,
                [FromServices] IFeatureService<VacationsRequestInput> service,
                CancellationToken cancellationToken) =>
        {
            var input = new VacationsRequestInput
            {
                EmployeeId = httpContext.GetUserId(),
                Dates = dates
            };

            validator.ValidateAndThrow(input);

            await service.Execute(input, cancellationToken);

            return Results.Ok();
        })
        .RequireAuthorization()
        .WithTags("Employees Endpoints");
    }
}