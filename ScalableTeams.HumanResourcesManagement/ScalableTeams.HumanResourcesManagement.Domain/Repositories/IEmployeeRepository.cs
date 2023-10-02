using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IEmployeeRepository : IRepository
{
    Task<Employee?> GetEmployeeAndManagerByEmployeeId(Guid id);
}
