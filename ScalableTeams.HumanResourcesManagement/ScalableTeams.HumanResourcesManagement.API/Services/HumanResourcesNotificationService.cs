using Microsoft.AspNetCore.SignalR;
using ScalableTeams.HumanResourcesManagement.API.Hubs;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.Services;

public class HumanResourcesNotificationService : IHumanResourcesNotificationService
{
    private readonly IHubContext<HumanResourcesHub> context;

    public HumanResourcesNotificationService(IHubContext<HumanResourcesHub> context)
    {
        this.context = context;
    }

    public async Task SendNewVacationRequestNotification(VacationRequest request, CancellationToken cancellationToken)
    {
        await context.Clients
            .Group(HubsGroups.HumanResources)
            .SendAsync("NewVacationsRequest", request, cancellationToken);

    }
}
