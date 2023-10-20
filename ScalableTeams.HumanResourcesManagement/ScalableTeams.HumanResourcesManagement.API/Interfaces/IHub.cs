using Microsoft.AspNetCore.Builder;

namespace ScalableTeams.HumanResourcesManagement.API.Interfaces;

public interface IHub
{
    void AddHub(WebApplication builder);
}
