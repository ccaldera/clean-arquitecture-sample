using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequestRejected;

public class VacationsRequestRejectedHandler : DomainEventHandlerBase<VacationRequestRejected>
{
    private readonly IEmployeesNotificationService _employeesNotificationService;

    public VacationsRequestRejectedHandler(IEmployeesNotificationService employeesNotificationService)
    {
        _employeesNotificationService = employeesNotificationService;
    }

    public override async Task Handle(VacationRequestRejected @event, CancellationToken cancellationToken)
    {
        await _employeesNotificationService.SendVacationRequestUpdateNotification(@event.VacationRequest, cancellationToken);
    }
}
