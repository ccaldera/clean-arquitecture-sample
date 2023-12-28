using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests;

public class NotifyEmployeeHandler : DomainEventHandlerBase<VacationRequestApprovedByHumanResources>
{
    private readonly IEmployeesNotificationService _employeesNotificationService;

    public NotifyEmployeeHandler(IEmployeesNotificationService employeesNotificationService)
    {
        _employeesNotificationService = employeesNotificationService;
    }

    public override async Task Handle(VacationRequestApprovedByHumanResources @event, CancellationToken cancellationToken)
    {
        await _employeesNotificationService.SendVacationRequestUpdateNotification(@event.VacationRequest, cancellationToken);
    }
}
