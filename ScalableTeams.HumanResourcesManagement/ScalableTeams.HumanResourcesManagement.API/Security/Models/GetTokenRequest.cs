namespace ScalableTeams.HumanResourcesManagement.API.Security.Models;

public class GetTokenRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
