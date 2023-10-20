using Microsoft.AspNetCore.SignalR;

namespace ScalableTeams.HumanResourcesManagement.API.Hubs;

public class NameUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        //for example just return the user's username
        return connection.User?.Identity?.Name!;
    }
}
