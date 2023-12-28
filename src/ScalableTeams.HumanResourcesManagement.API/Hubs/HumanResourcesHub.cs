using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ScalableTeams.HumanResourcesManagement.API.Hubs;

public class HumanResourcesHub : Hub
{
    public async Task JoinGroup()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, HubsGroups.HumanResources);
    }
}