using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Utilities;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public DepartmentsRepository(HumanResourcesManagementContext context)
    {
        this.dbContext = context;
    }

    public async Task<bool> EmployeeBelongsToDepartment(Guid employeeId, Departments department)
    {
        var departmentValue = department.GetDescription();

        return await dbContext.Employees
            .AnyAsync(x =>
                x.Department.Name == departmentValue
                && x.Id == employeeId);
    }
}