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

        try
        {
            return Handle(tEvent, cancellationToken);
        }
        catch (Exception)
        {
        }

        return Task.CompletedTask;
    }

    public abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
}