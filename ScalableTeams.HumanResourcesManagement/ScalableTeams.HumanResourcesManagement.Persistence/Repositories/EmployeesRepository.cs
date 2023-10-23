using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Persistence.Extensions;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class EmployeesRepository : RepositoryBase, IEmployeesRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public EmployeesRepository(
        HumanResourcesManagementContext dbContext,
        IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
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

    protected override IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return dbContext.GetDomainEvents();
    }

    protected override async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }
}
