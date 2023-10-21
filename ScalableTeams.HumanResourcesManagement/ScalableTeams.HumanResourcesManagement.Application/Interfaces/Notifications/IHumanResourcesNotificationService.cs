using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;

public interface IHumanResourcesNotificationService : INotificationService
{
    Task SendNewVacationRequestNotification(VacationRequest request, CancellationToken cancellationToken);
}
