using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.Hubs;

public class HumanResourcesHub : Hub
{
    public async Task JoinGroup()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, HubsGroups.HumanResources);
    }
}