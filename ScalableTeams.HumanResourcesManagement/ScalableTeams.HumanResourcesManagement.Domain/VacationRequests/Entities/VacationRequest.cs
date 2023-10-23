using ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

public class VacationRequest : Entity
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

        AddDomianEvent(new VacationRequestCreated(this));
    }

    public void ManagerApproves()
    {
        ManagerReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.ApprovedByManager;

        AddDomianEvent(new VacationRequestApprovedByManager(this));
    }

    public void ManagerRejects()
    {
        ManagerReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.RejectedByManager;

        AddDomianEvent(new VacationRequestRejected(this));
    }

    public void HumanResourcesApprovesRequest()
    {
        HrReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.ApprovedByHumanResources;

        AddDomianEvent(new VacationRequestApprovedByHumanResources(this));
    }

    public void HumanResourcesRejectsRequest()
    {
        HrReviewDate = DateTime.UtcNow;
        Status = VactionRequestsStatus.RejectedByHumanResources;

        AddDomianEvent(new VacationRequestRejected(this));
    }
}
