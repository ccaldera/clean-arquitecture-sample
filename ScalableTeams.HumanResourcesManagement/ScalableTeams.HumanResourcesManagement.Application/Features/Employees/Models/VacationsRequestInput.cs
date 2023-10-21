namespace ScalableTeams.HumanResourcesManagement.Application.Features.Employees.Models;

public class VacationsRequestInput
{
    public required Guid EmployeeId { get; init; }
    public required List<DateTime> Dates { get; init; }
}
