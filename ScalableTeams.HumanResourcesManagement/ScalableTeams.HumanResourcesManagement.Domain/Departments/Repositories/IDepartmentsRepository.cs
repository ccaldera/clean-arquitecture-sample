using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Enums;

namespace ScalableTeams.HumanResourcesManagement.Domain.Departments.Repositories;

public interface IDepartmentsRepository : IRepository
{
    Task<bool> EmployeeBelongsToDepartment(Guid employeeId, DepartmentType department);
}
