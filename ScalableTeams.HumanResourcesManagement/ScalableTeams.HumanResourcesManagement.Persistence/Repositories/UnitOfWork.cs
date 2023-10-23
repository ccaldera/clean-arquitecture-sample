using Microsoft.EntityFrameworkCore.Storage;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;
using ScalableTeams.HumanResourcesManagement.Persistence.Extensions;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class UnitOfWork : UnitOfWorkBase
{
    private readonly HumanResourcesManagementContext dbContext;
    private IDbContextTransaction? transaction;

    public UnitOfWork(
        HumanResourcesManagementContext dbContext,
        IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
    {
        this.dbContext = dbContext;

        EmployeesRepository = new EmployeesRepository(dbContext, eventDispatcher);
        DepartmentsRepository = new DepartmentsRepository(dbContext, eventDispatcher);
        VacationsRequestRepository = new VacationsRequestRepository(dbContext, eventDispatcher);
    }

    public override IEmployeesRepository EmployeesRepository { get; }
    public override IDepartmentsRepository DepartmentsRepository { get; }
    public override IVacationsRequestRepository VacationsRequestRepository { get; }


    public override async Task BeginTransaction()
    {
        transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public override async Task CommitTransaction()
    {
        await transaction!.CommitAsync();
    }

    public override async Task RollBackTransaction()
    {
        await transaction!.RollbackAsync();
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
