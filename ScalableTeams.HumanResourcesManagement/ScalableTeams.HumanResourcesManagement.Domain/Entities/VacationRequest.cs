using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

namespace ScalableTeams.HumanResourcesManagement.Domain.Entities;

public class VacationRequest
{
    public Guid Id { get; set; }
    public required Guid EmployeeId { get; set; }
    public required Employee Employee { get; set; }
    public DateTime? ManagerReviewDate { get; set; }
    public DateTime? HrReviewDate { get; set; }
    public ProcessStatus Status { get; set; }
    public List<DateTime> Dates { get; set; } = new List<DateTime>();

    public static VacationRequest NewRequest(Employee employee, List<DateTime> dates)
    {
        var request = new VacationRequest
        {
            Id = Guid.NewGuid(),
            Employee = employee,
            EmployeeId = employee.Id,
            Dates = dates
        };
        
        ValidateRequest(request);

        request.Status = ProcessStatus.CreatedByEmployee;

        return request;
    }

    private static void ValidateRequest(VacationRequest request)
    {
        var errors = new List<Error>();

        if (request.EmployeeId == Guid.Empty)
        {
            errors.Add(new Error(nameof(EmployeeId), "EmployeeId cannot be empty."));
        }

        if (!request.Dates.Any())
        {
            errors.Add(new Error(nameof(Dates), "The dates list cannot be empty."));
        }

        if (request.Dates.Any(x => x.Date <= DateTime.UtcNow.Date))
        {
            errors.Add(new Error(nameof(Dates), "Dates must be greater than today."));
        }

        if (request.Dates.Any(x => (x.Date - DateTime.UtcNow.Date).Days < 14))
        {
            errors.Add(new Error(nameof(Dates), "You cannot request vacations for the next 14 days."));
        }

        if (request.Dates.Any(x => (x.Date - DateTime.UtcNow.Date).Days > 365))
        {
            errors.Add(new Error(nameof(Dates), "You cannot request vacations with a date greater than 120 days."));
        }

        if (request.Dates.GroupBy(x => x.Date).Any(x => x.Count() > 1))
        {
            errors.Add(new Error(nameof(Dates), "There are some duplicated dates in the request."));
        }

        if (errors.Any())
        {
            throw new BusinessLogicExceptions(errors);
        }
    }

    public void ManagerApproves(Employee manager)
    {
        if(Employee.ManagerId != manager.Id)
        {
            throw new BusinessLogicException("Only the employee's direct manager can approve this request");
        }

        ManagerReviewDate = DateTime.UtcNow;
        Status = ProcessStatus.ManagerApproves;
    }

    public void ManagerRejects(Employee manager)
    {
        if (Employee.ManagerId != manager.Id)
        {
            throw new BusinessLogicException("Only the employee's direct manager can approve this request");
        }

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
