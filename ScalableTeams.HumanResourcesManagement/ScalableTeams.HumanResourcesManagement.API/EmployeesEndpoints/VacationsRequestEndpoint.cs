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

namespace ScalableTeams.HumanResourcesManagement.API.EmployeesEndpoints;

public class VacationsRequestEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/employees/{employeeId}/requests/daysoff", async (
                [FromRoute] Guid employeeId,
                [FromBody] List<DateTime> dates,
                [FromServices] VacationsRequestService service,
                CancellationToken cancellationToken) =>
        {
            var command = new VacationsRequestInput
            {
                EmployeeId = employeeId,
                Dates = dates
            };

            var result = await service.Execute(command, cancellationToken);

            return Results.Ok(result);
        })
        .Produces<OperationResponses>()
        .WithTags("EmployeesEndpoints");
    }
}


