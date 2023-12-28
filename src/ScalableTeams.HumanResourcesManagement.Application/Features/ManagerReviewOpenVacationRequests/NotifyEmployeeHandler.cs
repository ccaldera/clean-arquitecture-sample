using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests;

public class NotifyEmployeeHandler : DomainEventHandlerBase<VacationRequestApprovedByManager>
{
    private readonly IEmployeesNotificationService _employeesNotificationService;

    public NotifyEmployeeHandler(IEmployeesNotificationService employeesNotificationService)
    {
        _employeesNotificationService = employeesNotificationService;
    }

    public override async Task Handle(VacationRequestApprovedByManager @event, CancellationToken cancellationToken)
    {
        await _employeesNotificationService.SendVacationRequestUpdateNotification(@event.VacationRequest, cancellationToken);
    }
}
