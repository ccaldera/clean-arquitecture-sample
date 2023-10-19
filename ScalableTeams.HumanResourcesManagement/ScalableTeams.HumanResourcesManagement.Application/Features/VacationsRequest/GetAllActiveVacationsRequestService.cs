using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class GetAllActiveVacationsRequestService : IFeatureService<GetAllActiveVacationsRequestInput, GetAllActiveVacationsRequestResult>
{
    private readonly IVacationsRequestRepository vacationsRequestRepository;

    public GetAllActiveVacationsRequestService(IVacationsRequestRepository vacationsRequestRepository)
    {
        this.vacationsRequestRepository = vacationsRequestRepository;
    }

    public Task<GetAllActiveVacationsRequestResult> Execute(GetAllActiveVacationsRequestInput input, CancellationToken cancellationToken)
    {
        var requests = vacationsRequestRepository
            .GetAllVacationsRequests()
            .Where(x => 
                x.Status == ProcessStatus.CreatedByEmployee
                && x.ManagerId == input.ManagerId)
            .ToList();


        var result = new GetAllActiveVacationsRequestResult
        {
            Requests = requests
        };

        return Task.FromResult(result);
    }
}
