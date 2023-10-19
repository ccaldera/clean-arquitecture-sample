using ScalableTeams.HumanResourcesManagement.Domain.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IDepartmentsRepository : IRepository
{
    Task<bool> EmployeeBelongsToDepartment(Guid employeeId, Departments department);
}
