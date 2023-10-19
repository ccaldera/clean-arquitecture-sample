using ScalableTeams.HumanResourcesManagement.Domain.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.Entities;

public class VacationRequest
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = default!;
    public DateTime? ManagerReviewDate { get; private set; }
    public DateTime? HrReviewDate { get; private set; }
    public ProcessStatus Status { get; private set; }
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
        Status = ProcessStatus.CreatedByEmployee;
    }

    public void ManagerApproves()
    {
        ManagerReviewDate = DateTime.UtcNow;
        Status = ProcessStatus.ManagerApproves;
    }

    public void ManagerRejects()
    {
        ManagerReviewDate = DateTime.UtcNow;
        Status = ProcessStatus.ManagerRejects;
    }

    private void HrApproves()
    {
        HrReviewDate = DateTime.UtcNow;
        Status = ProcessStatus.HrApproves;
    }

    private void HrRejects()
    {
        HrReviewDate = DateTime.UtcNow;
        Status = ProcessStatus.HrRejects;
    }
}
