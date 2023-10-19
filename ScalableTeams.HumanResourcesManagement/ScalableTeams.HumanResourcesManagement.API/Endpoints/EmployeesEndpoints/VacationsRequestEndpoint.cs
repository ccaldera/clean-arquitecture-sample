using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.Application.Features;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using System;
using System.Collections.Generic;
using System.Threading;

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
        app.MapPost("api/employees/{employeeId}/requests/vacations",
            async (
                [FromRoute] Guid employeeId,
                [FromBody] List<DateTime> dates,
                [FromServices] IFeatureService<VacationsRequestInput> service,
                CancellationToken cancellationToken) =>
        {
            var input = new VacationsRequestInput
            {
                EmployeeId = employeeId,
                Dates = dates
            };

            validator.ValidateAndThrow(input);

            await service.Execute(input, cancellationToken);

            return Results.Ok();
        })
        .WithTags("Employees Endpoints");
    }
}