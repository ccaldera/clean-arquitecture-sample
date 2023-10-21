using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;

public interface IManagersNotificationService : INotificationService
{
    Task SendNewVacationRequestNotification(Guid managerId, VacationRequest request, CancellationToken cancellationToken);
}
