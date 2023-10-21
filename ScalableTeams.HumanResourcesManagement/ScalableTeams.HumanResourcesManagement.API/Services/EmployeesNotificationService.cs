﻿using Microsoft.AspNetCore.SignalR;
using ScalableTeams.HumanResourcesManagement.API.Hubs;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.Services;

public class EmployeesNotificationService : IEmployeesNotificationService
{
    private readonly IHubContext<EmployeesHub> context;

    public EmployeesNotificationService(IHubContext<EmployeesHub> context)
    {
        this.context = context;
    }

    public async Task SendVacationResultNotification(VacationRequest request, CancellationToken cancellationToken)
    {
        await context.Clients
            .Users(request.EmployeeId.ToString())
            .SendAsync("VacationRequestUpdate", request, cancellationToken);
    }
}
