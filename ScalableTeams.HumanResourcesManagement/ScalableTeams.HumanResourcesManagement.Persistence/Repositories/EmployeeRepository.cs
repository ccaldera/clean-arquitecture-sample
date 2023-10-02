using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly HumanResourcesManagementContext context;

    public EmployeeRepository(HumanResourcesManagementContext context)
    {
        this.context = context;
    }

    public async Task<Employee?> GetEmployeeAndManagerByEmployeeId(Guid id)
    {
        return await context
            .Employees
            .Include(x => x.Manager)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
