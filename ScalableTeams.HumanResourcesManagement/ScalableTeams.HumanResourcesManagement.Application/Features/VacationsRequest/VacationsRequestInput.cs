namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestInput
{
    public required Guid EmployeeId { get; init; }
    public required List<DateTime> Dates { get; init; }
}
