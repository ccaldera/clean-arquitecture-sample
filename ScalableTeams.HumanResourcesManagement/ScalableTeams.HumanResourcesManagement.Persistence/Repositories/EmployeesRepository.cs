using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public EmployeesRepository(HumanResourcesManagementContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Employee?> Get(Guid id)
    {
        return await dbContext
            .Employees
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Employee?> GetUserByEmailAndPassword(string email, string password)
    {
        return await dbContext
            .Employees
            .Include(x => x.Department)
            .Where(x => x.Email == email && x.Password == password)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsManager(Guid id)
    {
        return await dbContext
            .Employees
            .AnyAsync(x => x.ManagerId == id);
    }
}
