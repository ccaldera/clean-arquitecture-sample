using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;

public abstract class RepositoryBase : IRepository
{
    protected IEventDispatcher EventDispatcher { get; private set; }

    public RepositoryBase(IEventDispatcher eventDispatcher)
    {
        EventDispatcher = eventDispatcher;
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await Save();

        IEnumerable<IDomainEvent> domainEvents = GetDomainEvents();
        IEnumerable<Task> tasks = domainEvents
            .Select(x => EventDispatcher.Dispatch(x, cancellationToken));

        await Task.WhenAll(tasks);
    }

    protected abstract IEnumerable<IDomainEvent> GetDomainEvents();
    protected abstract Task Save();
}
