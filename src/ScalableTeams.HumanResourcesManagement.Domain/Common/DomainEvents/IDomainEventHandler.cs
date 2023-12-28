namespace ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

public interface IDomainEventHandler<in TEvent> : IDomainEventHandler where TEvent : IDomainEvent
{
    Task Handle(TEvent @event, CancellationToken cancellationToken);
}

public interface IDomainEventHandler
{
    Task Handle(IDomainEvent @event, CancellationToken cancellationToken);
}
