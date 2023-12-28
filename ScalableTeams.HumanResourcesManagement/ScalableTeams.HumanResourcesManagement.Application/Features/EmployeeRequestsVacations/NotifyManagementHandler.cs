using ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.EmployeeRequestsVacations;

public class NotifyManagementHandler : DomainEventHandlerBase<VacationRequestCreated>
{
    private readonly IManagersNotificationService _managerNotificationService;

    public NotifyManagementHandler(IManagersNotificationService managerNotificationService)
    {
        _managerNotificationService = managerNotificationService;
    }

    public override async Task Handle(VacationRequestCreated domainEvent, CancellationToken cancellationToken)
    {
        await _managerNotificationService.SendNewVacationRequestNotification(
            domainEvent.VacationRequest.Employee.ManagerId!.Value,
            domainEvent.VacationRequest,
            cancellationToken);
    }
}
