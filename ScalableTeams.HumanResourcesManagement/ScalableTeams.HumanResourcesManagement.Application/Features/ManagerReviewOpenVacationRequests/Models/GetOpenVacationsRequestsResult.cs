using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.ValueObjects;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests.Models;

public class GetOpenVacationsRequestsResult
{
    public List<VacationsRequestReview> Requests { get; set; } = [];
}
