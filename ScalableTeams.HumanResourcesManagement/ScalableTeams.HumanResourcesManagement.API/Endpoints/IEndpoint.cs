using Microsoft.AspNetCore.Routing;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}