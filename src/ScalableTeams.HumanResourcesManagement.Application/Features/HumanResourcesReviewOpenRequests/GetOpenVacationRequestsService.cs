using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests;

public class GetOpenVacationRequestsService : IFeatureService<GetOpenVacationRequestsInput, GetOpenVacationRequestsResult>
{
    private readonly IVacationsRequestRepository _vacationsRequestRepository;

    public GetOpenVacationRequestsService(IVacationsRequestRepository vacationsRequestRepository)
    {
        _vacationsRequestRepository = vacationsRequestRepository;
    }

    public Task<GetOpenVacationRequestsResult> Execute(GetOpenVacationRequestsInput input, CancellationToken cancellationToken)
    {
        var requests = _vacationsRequestRepository
            .GetAllVacationsRequests()
            .Where(x => x.Status == VactionRequestsStatus.ApprovedByManager)
            .ToList();

        var result = new GetOpenVacationRequestsResult
        {
            Requests = requests
        };

        return Task.FromResult(result);
    }
}
