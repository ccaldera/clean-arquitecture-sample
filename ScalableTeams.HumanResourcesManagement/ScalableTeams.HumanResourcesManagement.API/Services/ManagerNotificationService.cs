using Microsoft.AspNetCore.SignalR;
using ScalableTeams.HumanResourcesManagement.API.Hubs;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.Services;

public class ManagerNotificationService : IManagerNotificationService
{
    private readonly IHubContext<ManagersHub> context;

    public ManagerNotificationService(IHubContext<ManagersHub> context)
    {
        this.context = context;
    }

    public async Task SendNewVacationRequestNotification(Guid managerId, VacationRequest request, CancellationToken cancellationToken)
    {
        await context.Clients
            .User(managerId.ToString())
            .SendAsync("NewVacationsRequest", request, cancellationToken);
    }
}
