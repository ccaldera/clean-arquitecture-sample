using ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

public class VacationRequest
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = default!;
    public DateTime? ManagerReviewDate { get; private set; }
    public DateTime? HrReviewDate { get; private set; }
    public VactionRequestsStatus Status { get; private set; }
    public List<DateTime> Dates { get; private set; } = new List<DateTime>();

    private VacationRequest()
    {
    }

    public VacationRequest(Employee? employee, List<DateTime> dates)
    {
        Employee = employee
            ?? throw new ArgumentNullException(nameof(employee));

        Dates = dates
            ?? throw new ArgumentNullException(nameof(dates));

        Id = Guid.NewGuid();
        Employee = employee;
        EmployeeId = employee.Id;
        Dates = dates;
        Status = VactionRequestsStatus.CreatedByEmployee;
    }

    public void ManagerApproves()
    {
        ManagerReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.ApprovedByManager;
    }

    public void ManagerRejects()
    {
        ManagerReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.RejectedByManager;
    }

    public void HrApproves()
    {
        HrReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.ApprovedByHumanResources;
    }

    public void HrRejects()
    {
        HrReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.RejectedByHumanResources;
    }
}
