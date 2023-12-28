using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;

public interface IEmployeesNotificationService : INotificationService
{
    Task SendVacationRequestUpdateNotification(VacationRequest request, CancellationToken cancellationToken);
}
