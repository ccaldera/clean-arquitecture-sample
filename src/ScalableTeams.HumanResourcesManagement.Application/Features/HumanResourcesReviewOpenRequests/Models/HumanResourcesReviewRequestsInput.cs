using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;

public class HumanResourcesReviewRequestsInput
{
    public Guid HrEmployeeId { get; set; }
    public Guid VacationRequestId { get; set; }
    public VactionRequestsStatus NewStatus { get; set; }
}
