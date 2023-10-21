using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.ValueObjects;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

public interface IVacationsRequestRepository : IRepository
{
    Task Insert(VacationRequest vacationRequest);

    Task<VacationRequest?> Get(Guid id);

    IQueryable<VacationsRequestReview> GetAllVacationsRequests();

    Task SaveChanges();
}
