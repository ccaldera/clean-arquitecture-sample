using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using ScalableTeams.HumanResourcesManagement.API.Interfaces;
using ScalableTeams.HumanResourcesManagement.API.Security;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints;

public class ManagersHub : Hub, IHub
{
    public void AddHub(WebApplication builder)
    {
        builder
            .MapHub<ManagersHub>("/managers")
            .RequireAuthorization(SecurityPolicies.ManagersPolicy);
    }
}