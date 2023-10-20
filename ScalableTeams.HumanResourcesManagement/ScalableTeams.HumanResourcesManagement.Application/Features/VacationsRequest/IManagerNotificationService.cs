using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public interface IManagerNotificationService : INotificationService
{
    Task SendNewVacationRequestNotification(Guid managerId, VacationRequest request, CancellationToken cancellationToken);
}
