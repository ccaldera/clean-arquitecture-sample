using ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests;

public class ManagerReviewRequestService : IFeatureService<ManagerReviewRequestInput>
{
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IEmployeesRepository employeesRepository;

    public ManagerReviewRequestService(
        IVacationsRequestRepository vacationsRequestRepository,
        IEmployeesRepository employeesRepository)
    {
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.employeesRepository = employeesRepository;
    }

    public async Task Execute(ManagerReviewRequestInput input, CancellationToken cancellationToken)
    {
        var vacationsRequest = await vacationsRequestRepository.Get(input.VacationRequestId)
            ?? throw new ResourceNotFoundException($"Vacation request with id {input.VacationRequestId} was not found");

        var employee = await employeesRepository.Get(vacationsRequest.EmployeeId)
            ?? throw new ResourceNotFoundException($"The employee {vacationsRequest.EmployeeId} related to this request does not exists.");

        if (employee.ManagerId != input.ReviewerId)
        {
            throw new BusinessLogicException("Only the employee's manager can review this request.");
        }

        switch (input.NewStatus)
        {
            case VactionRequestsStatus.ApprovedByManager:
                vacationsRequest.ManagerApproves();
                break;
            case VactionRequestsStatus.RejectedByManager:
                vacationsRequest.ManagerRejects();
                break;
            default:
                throw new BusinessLogicException("Managers can only approve or reject requests.");
        }

        await vacationsRequestRepository.SaveChanges(cancellationToken);
    }
}
