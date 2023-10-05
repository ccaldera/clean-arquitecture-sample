using ScalableTeams.HumanResourcesManagement.Domain.Agreggates;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;

public class GetAllActiveVacationsRequestResult
{
    public List<VacationsRequestReview> Requests { get; set; } = new List<VacationsRequestReview>();
}
