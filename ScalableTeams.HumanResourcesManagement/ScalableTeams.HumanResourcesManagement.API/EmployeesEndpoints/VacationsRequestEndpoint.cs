using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Common;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.EmployeesEndpoints;

public class VacationsRequestEndpoint : IEndpoint
{
    private readonly IMediator mediator;

    public VacationsRequestEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employees/{employeeId}/requests/daysoff", async (
                [FromRoute] Guid employeeId, 
                [FromBody] List<DateTime> dates) =>
        {
            return await HandleAsync(employeeId, dates);
        })
        .Produces<VacationsRequestResult>()
        .WithTags("EmployeeEndpoints");
    }

    public async Task<IResult> HandleAsync(
        Guid employeeId, 
        List<DateTime> dates)
    {
        var command = new VacationsRequestCommand
        {
            EmployeeId = employeeId,
            Dates = dates
        };

        var result = await mediator.Send(command);

        return Results.Ok(result);
    }
}


