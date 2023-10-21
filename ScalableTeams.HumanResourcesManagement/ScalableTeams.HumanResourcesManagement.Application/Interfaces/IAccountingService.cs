using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces;

public interface IAccountingService
{
    Task NotifyVacationsRequest(VacationRequest vacationRequest, CancellationToken cancellationToken);
}
