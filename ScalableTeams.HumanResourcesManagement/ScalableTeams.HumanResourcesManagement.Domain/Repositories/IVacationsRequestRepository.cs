using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IVacationsRequestRepository : IRepository
{
    Task Insert(VacationRequest vacationRequest);

    Task SaveChanges();
}
