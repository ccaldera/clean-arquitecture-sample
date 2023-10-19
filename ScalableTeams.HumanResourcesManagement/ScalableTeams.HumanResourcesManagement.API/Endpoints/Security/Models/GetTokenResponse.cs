namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Models;

public class GetTokenResponse
{
    public required string Token { get; set; }
    public required int ExpirationTimeInMinutes { get; set; }
}
