using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests;

public class NotifyHumanResourcesHandler : DomainEventHandlerBase<VacationRequestApprovedByManager>
{
    private readonly IHumanResourcesNotificationService _humanResourcesNotificationService;

    public NotifyHumanResourcesHandler(IHumanResourcesNotificationService humanResourcesNotificationService)
    {
        _humanResourcesNotificationService = humanResourcesNotificationService;
    }

    public override async Task Handle(VacationRequestApprovedByManager @event, CancellationToken cancellationToken)
    {
        await _humanResourcesNotificationService.SendNewVacationRequestNotification(@event.VacationRequest, cancellationToken);
    }
}
