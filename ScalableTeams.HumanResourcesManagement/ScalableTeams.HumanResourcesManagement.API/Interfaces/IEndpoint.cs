using Microsoft.AspNetCore.Routing;

namespace ScalableTeams.HumanResourcesManagement.API.Interfaces;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}