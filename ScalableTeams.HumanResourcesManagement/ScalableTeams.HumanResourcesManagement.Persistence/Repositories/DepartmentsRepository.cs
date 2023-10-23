using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Utilities;
using ScalableTeams.HumanResourcesManagement.Persistence.Extensions;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class DepartmentsRepository : RepositoryBase, IDepartmentsRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public DepartmentsRepository(
        HumanResourcesManagementContext dbContext,
        IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> EmployeeBelongsToDepartment(Guid employeeId, DepartmentType department)
    {
        var departmentValue = department.GetDescription();

        return await dbContext.Employees
            .AnyAsync(x =>
                x.Department.Name == departmentValue
                && x.Id == employeeId);
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