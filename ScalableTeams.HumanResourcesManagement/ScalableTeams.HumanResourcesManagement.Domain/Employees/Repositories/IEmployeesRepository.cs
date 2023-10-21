using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;

public interface IEmployeesRepository : IRepository
{
    Task<Employee?> Get(Guid id);

    Task<Employee?> GetUserByEmailAndPassword(string email, string password);

    Task<bool> IsManager(Guid id);
}
