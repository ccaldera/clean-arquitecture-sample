using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;

public abstract class Entity
{
    private readonly Queue<IDomainEvent> domainEvents = new();

    protected void AddDomianEvent(IDomainEvent domainEvent)
    {
        domainEvents.Enqueue(domainEvent);
    }

    public IEnumerable<IDomainEvent> PopDomainEvents()
    {
        while (domainEvents.Any())
        {
            yield return domainEvents.Dequeue();
        }
    }
}
