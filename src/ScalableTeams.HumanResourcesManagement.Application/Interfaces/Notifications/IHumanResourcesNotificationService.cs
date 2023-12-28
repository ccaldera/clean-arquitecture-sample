using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;

public interface IHumanResourcesNotificationService : INotificationService
{
    Task SendNewVacationRequestNotification(VacationRequest request, CancellationToken cancellationToken);
}
