using ScalableTeams.HumanResourcesManagement.Domain.Agreggates;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;

public class GetAllActiveVacationsRequestResult
{
    public List<VacationsRequestReview> Requests { get; set; } = new List<VacationsRequestReview>();
}
