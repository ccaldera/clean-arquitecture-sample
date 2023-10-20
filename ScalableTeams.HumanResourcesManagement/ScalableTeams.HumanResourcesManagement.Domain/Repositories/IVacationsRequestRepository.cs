using ScalableTeams.HumanResourcesManagement.Domain.Agreggates;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IVacationsRequestRepository : IRepository
{
    Task Insert(VacationRequest vacationRequest);

    Task<VacationRequest?> Get(Guid id);

    IQueryable<VacationsRequestReview> GetAllVacationsRequests();

    Task SaveChanges();
}
