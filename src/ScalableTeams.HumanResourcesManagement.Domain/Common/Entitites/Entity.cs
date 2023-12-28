using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;

public abstract class Entity
{
    private readonly Queue<IDomainEvent> _domainEvents = new();

    protected void AddDomianEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Enqueue(domainEvent);
    }

    public IEnumerable<IDomainEvent> PopDomainEvents()
    {
        while (_domainEvents.Count != 0)
        {
            yield return _domainEvents.Dequeue();
        }
    }
}
