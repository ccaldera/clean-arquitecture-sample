using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.Common;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}