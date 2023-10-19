using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IEmployeesRepository : IRepository
{
    Task<Employee?> Get(Guid id);
}
