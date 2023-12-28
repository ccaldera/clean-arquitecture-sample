using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces;

public interface IAccountingService
{
    Task NotifyVacationsRequest(VacationRequest vacationRequest, CancellationToken cancellationToken);
}
