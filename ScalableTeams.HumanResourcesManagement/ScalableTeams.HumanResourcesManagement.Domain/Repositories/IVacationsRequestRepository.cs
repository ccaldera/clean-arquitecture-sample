using ScalableTeams.HumanResourcesManagement.Domain.Agreggates;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IVacationsRequestRepository : IRepository
{
    Task Insert(VacationRequest vacationRequest);

    IEnumerable<VacationsRequestReview> GetAllVacationsRequests();

    Task SaveChanges();
}
