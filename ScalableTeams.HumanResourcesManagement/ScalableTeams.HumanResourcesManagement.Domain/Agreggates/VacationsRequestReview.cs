using ScalableTeams.HumanResourcesManagement.Domain.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.Agreggates;

public class VacationsRequestReview
{
    public Guid Id { get; set; }
    public required Guid EmployeeId { get; set; }
    public required Guid? ManagerId { get; set; }
    public required string EmployeeName { get; set; }
    public required string EmployeeLastName { get; set; }
    public DateTime? ManagerReviewDate { get; set; }
    public DateTime? HrReviewDate { get; set; }
    public ProcessStatus Status { get; set; }
    public List<DateTime> Dates { get; set; } = new List<DateTime>();
}
