using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public interface IHumanResourcesNotificationService : INotificationService
{
    Task SendNewVacationRequestNotification(VacationRequest request, CancellationToken cancellationToken);
}
