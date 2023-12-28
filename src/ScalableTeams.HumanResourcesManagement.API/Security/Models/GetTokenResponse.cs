namespace ScalableTeams.HumanResourcesManagement.API.Security.Models;

public class GetTokenResponse
{
    public required string Token { get; set; }
    public required int ExpirationTimeInMinutes { get; set; }
}
