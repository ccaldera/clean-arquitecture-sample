using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces.Notifications;

public interface IEmployeesNotificationService : INotificationService
{
    Task SendVacationResultNotification(VacationRequest request, CancellationToken cancellationToken);
}
