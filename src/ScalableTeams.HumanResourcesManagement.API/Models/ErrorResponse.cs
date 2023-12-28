namespace ScalableTeams.HumanResourcesManagement.API.Models;

public class ErrorResponse
{
    public string Message { get; }

    public ErrorResponse(string message)
    {
        Message = message;
    }
}
