namespace ScalableTeams.HumanResourcesManagement.API.Configuration;

public class JwtConfiguration
{
    public required string Secret { get; set; }
    public required int ExpirationInMinutes { get; set; }
}