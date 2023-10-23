using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests;

public class HumanResourcesReviewRequestsService : IFeatureService<HumanResourcesReviewRequestsInput>
{
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IDepartmentsRepository departmentsRepository;

    public HumanResourcesReviewRequestsService(
        IVacationsRequestRepository vacationsRequestRepository,
        IDepartmentsRepository departmentsRepository)
    {
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.departmentsRepository = departmentsRepository;
    }

    public async Task Execute(HumanResourcesReviewRequestsInput input, CancellationToken cancellationToken)
    {
        var vacationsRequest = await vacationsRequestRepository.Get(input.VacationRequestId)
            ?? throw new ResourceNotFoundException($"Vacation request with id {input.VacationRequestId} was not found");

        var isHrEmployee = await departmentsRepository.EmployeeBelongsToDepartment(input.HrEmployeeId, DepartmentType.HumanResources);

        if (!isHrEmployee)
        {
            throw new BusinessLogicException("Only HR employees can review this request.");
        }

        switch (input.NewStatus)
        {
            case VactionRequestsStatus.ApprovedByHumanResources:
                vacationsRequest.HumanResourcesApprovesRequest();
                break;
            case VactionRequestsStatus.RejectedByHumanResources:
                vacationsRequest.HumanResourcesRejectsRequest();
                break;
            default:
                throw new BusinessLogicException("HR employees can only approve or reject requests.");
        }

        await vacationsRequestRepository.SaveChanges(cancellationToken);
    }
}
