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
    private readonly HumanResourcesManagementContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(
        HumanResourcesManagementContext dbContext,
        IEmployeesRepository employeesRepository,
        IDepartmentsRepository departmentsRepository,
        IVacationsRequestRepository vacationsRequestRepository,
        IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
    {
        _dbContext = dbContext;

        EmployeesRepository = employeesRepository;
        DepartmentsRepository = departmentsRepository;
        VacationsRequestRepository = vacationsRequestRepository;
    }

    public override IEmployeesRepository EmployeesRepository { get; }
    public override IDepartmentsRepository DepartmentsRepository { get; }
    public override IVacationsRequestRepository VacationsRequestRepository { get; }


    public override async Task BeginTransaction()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public override async Task CommitTransaction()
    {
        await _transaction!.CommitAsync();
    }

    public override async Task RollBackTransaction()
    {
        await _transaction!.RollbackAsync();
    }

    protected override IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return _dbContext.GetDomainEvents();
    }

    protected override async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}
