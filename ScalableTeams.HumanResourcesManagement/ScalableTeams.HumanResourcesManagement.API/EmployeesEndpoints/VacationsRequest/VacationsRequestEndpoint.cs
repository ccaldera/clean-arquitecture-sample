﻿using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Common;
using ScalableTeams.HumanResourcesManagement.Application.Common;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.EmployeesEndpoints.VacationsRequest;

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
                [FromServices] IFeatureService<VacationsRequestInput, VacationsRequestResult> service,
                CancellationToken cancellationToken) =>
        {
            var input = new VacationsRequestInput
            {
                EmployeeId = employeeId,
                Dates = dates
            };

            validator.ValidateAndThrow(input);

            var result = await service.Execute(
                input, 
                cancellationToken);

            return Results.Ok(result);
        })
        .Produces<VacationsRequestResult>()
        .WithTags("EmployeesEndpoints");
    }
}