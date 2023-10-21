namespace ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

public interface IEventDispatcher
{
    Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken);
}
