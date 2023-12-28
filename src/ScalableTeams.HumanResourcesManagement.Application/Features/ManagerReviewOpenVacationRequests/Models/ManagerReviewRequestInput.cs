using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests.Models;

public class ManagerReviewRequestInput
{
    public Guid ReviewerId { get; set; }
    public Guid VacationRequestId { get; set; }
    public VactionRequestsStatus NewStatus { get; set; }
}
