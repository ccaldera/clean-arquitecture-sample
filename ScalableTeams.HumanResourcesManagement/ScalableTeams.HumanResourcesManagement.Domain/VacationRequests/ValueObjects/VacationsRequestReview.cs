using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.ValueObjects;

public class VacationsRequestReview
{
    public Guid VacationRequestId { get; set; }
    public required Guid EmployeeId { get; set; }
    public required Guid? ManagerId { get; set; }
    public required string EmployeeName { get; set; }
    public required string EmployeeLastName { get; set; }
    public DateTime? ManagerReviewDate { get; set; }
    public DateTime? HrReviewDate { get; set; }
    public VactionRequestsStatus Status { get; set; }
    public List<DateTime> Dates { get; set; } = [];
}
