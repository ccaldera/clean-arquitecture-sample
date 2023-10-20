using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class GetAllHrActiveVacationsRequestService : IFeatureService<GetAllHrActiveVacationsRequestInput, GetAllHrActiveVacationsRequestResult>
{
    private readonly IVacationsRequestRepository vacationsRequestRepository;

    public GetAllHrActiveVacationsRequestService(IVacationsRequestRepository vacationsRequestRepository)
    {
        this.vacationsRequestRepository = vacationsRequestRepository;
    }

    public Task<GetAllHrActiveVacationsRequestResult> Execute(GetAllHrActiveVacationsRequestInput input, CancellationToken cancellationToken)
    {
        var requests = vacationsRequestRepository
            .GetAllVacationsRequests()
            .Where(x => x.Status == ProcessStatus.ManagerApproves)
            .ToList();

        var result = new GetAllHrActiveVacationsRequestResult
        {
            Requests = requests
        };

        return Task.FromResult(result);
    }
}
