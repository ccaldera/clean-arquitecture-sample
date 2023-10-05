namespace ScalableTeams.HumanResourcesManagement.API.Common.Models;

public class ErrorResponse
{
    public string Message { get; }

    public ErrorResponse(string message)
    {
        Message = message;
    }
}
