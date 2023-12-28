namespace ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

public class DomainEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        Type handlersListType = typeof(IEnumerable<>).MakeGenericType(handlerType);
        var handlers = (IEnumerable<IDomainEventHandler>)_serviceProvider.GetService(handlersListType)!;

        foreach (IDomainEventHandler handler in handlers)
        {
            if (handler != null)
            {
                await handler.Handle(domainEvent, cancellationToken);
            }
        }
    }
}
