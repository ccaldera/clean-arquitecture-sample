namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestResult
{
    public bool Success { get; set; }
    public List<string>? RejectionReasons { get; set; }

    public void SetSuccess()
    {
        Success = true;
        RejectionReasons = null;
    }
}
