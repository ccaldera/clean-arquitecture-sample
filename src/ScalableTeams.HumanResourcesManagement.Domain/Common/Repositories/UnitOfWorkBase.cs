using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;

public abstract class UnitOfWorkBase : IUnitOfWork
{
    protected IEventDispatcher EventDispatcher { get; private set; }

    public UnitOfWorkBase(IEventDispatcher eventDispatcher)
    {
        EventDispatcher = eventDispatcher;
    }

    public abstract IEmployeesRepository EmployeesRepository { get; }
    public abstract IDepartmentsRepository DepartmentsRepository { get; }
    public abstract IVacationsRequestRepository VacationsRequestRepository { get; }

    public abstract Task BeginTransaction();
    public abstract Task RollBackTransaction();
    public abstract Task CommitTransaction();
    protected abstract IEnumerable<IDomainEvent> GetDomainEvents();
    protected abstract Task Save();

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await Save();

        IEnumerable<IDomainEvent> domainEvents = GetDomainEvents();

        IEnumerable<Task> tasks = domainEvents
            .Select(x => EventDispatcher.Dispatch(x, cancellationToken));

        await Task.WhenAll(tasks);
    }
}
