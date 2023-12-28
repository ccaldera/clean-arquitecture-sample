using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.ValueObjects;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;

public class GetOpenVacationRequestsResult
{
    public List<VacationsRequestReview> Requests { get; set; } = [];
}
