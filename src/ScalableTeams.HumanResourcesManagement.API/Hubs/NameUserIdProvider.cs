using Microsoft.AspNetCore.SignalR;

namespace ScalableTeams.HumanResourcesManagement.API.Hubs;

public class NameUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Identity?.Name!;
    }
}
