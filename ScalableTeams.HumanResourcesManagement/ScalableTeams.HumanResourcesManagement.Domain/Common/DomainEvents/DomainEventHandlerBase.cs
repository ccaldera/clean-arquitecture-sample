namespace ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

public abstract class DomainEventHandlerBase<TEvent> : IDomainEventHandler<TEvent>
    where TEvent : class, IDomainEvent
{
    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent is not TEvent tEvent)
        {
            return Task.CompletedTask;
        }

        return Handle(tEvent, cancellationToken);
    }

    public abstract Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}