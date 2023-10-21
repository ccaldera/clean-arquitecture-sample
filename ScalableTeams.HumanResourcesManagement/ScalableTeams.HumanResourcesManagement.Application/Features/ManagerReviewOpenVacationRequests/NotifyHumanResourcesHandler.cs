using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests;

public class NotifyHumanResourcesHandler : DomainEventHandlerBase<VacationRequestApprovedByManager>
{
    private readonly IHumanResourcesNotificationService humanResourcesNotificationService;

    public NotifyHumanResourcesHandler(IHumanResourcesNotificationService humanResourcesNotificationService)
    {
        this.humanResourcesNotificationService = humanResourcesNotificationService;
    }

    public override async Task Handle(VacationRequestApprovedByManager @event, CancellationToken cancellationToken)
    {
        await humanResourcesNotificationService.SendNewVacationRequestNotification(@event.VacationRequest, cancellationToken);
    }
}
