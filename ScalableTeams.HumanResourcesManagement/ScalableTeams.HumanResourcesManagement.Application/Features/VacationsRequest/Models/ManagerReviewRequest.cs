using ScalableTeams.HumanResourcesManagement.Domain.Enums;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;

public class ManagerReviewRequest
{
    public Guid ReviewerId { get; set; }
    public Guid VacationRequestId { get; set; }
    public ProcessStatus NewStatus { get; set; }
}
