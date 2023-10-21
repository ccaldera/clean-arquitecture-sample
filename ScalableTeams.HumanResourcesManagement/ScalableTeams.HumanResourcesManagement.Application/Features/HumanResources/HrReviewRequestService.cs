using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResources.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResources;

public class HrReviewRequestService : IFeatureService<HrReviewRequest>
{
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IDepartmentsRepository departmentsRepository;
    private readonly IAccountingService accountingService;
    private readonly IEmployeesNotificationService employeesNotificationService;

    public HrReviewRequestService(
        IVacationsRequestRepository vacationsRequestRepository,
        IDepartmentsRepository departmentsRepository,
        IAccountingService accountingService,
        IEmployeesNotificationService employeesNotificationService)
    {
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.departmentsRepository = departmentsRepository;
        this.accountingService = accountingService;
        this.employeesNotificationService = employeesNotificationService;
    }

    public async Task Execute(HrReviewRequest input, CancellationToken cancellationToken)
    {
        var vacationsRequest = await vacationsRequestRepository.Get(input.VacationRequestId)
            ?? throw new ResourceNotFoundException($"Vacation request with id {input.VacationRequestId} was not found");

        var isHrEmployee = await departmentsRepository.EmployeeBelongsToDepartment(input.HrEmployeeId, Departments.HumanResources);

        if (!isHrEmployee)
        {
            throw new BusinessLogicException("Only HR employees can review this request.");
        }

        switch (input.NewStatus)
        {
            case ProcessStatus.HrApproves:
                vacationsRequest.HrApproves();
                break;
            case ProcessStatus.HrRejects:
                vacationsRequest.HrRejects();
                break;
            default:
                throw new BusinessLogicException("HR employees can only approve or reject requests.");
        }

        await vacationsRequestRepository.SaveChanges();

        var tasks = new List<Task>()
        {
            employeesNotificationService.SendVacationResultNotification(vacationsRequest, cancellationToken)
        };

        if(input.NewStatus == ProcessStatus.HrApproves)
        {
            tasks.Add(accountingService.NotifyVacationsRequest(vacationsRequest, cancellationToken));
        }

        await Task.WhenAll(tasks);
    }
}
