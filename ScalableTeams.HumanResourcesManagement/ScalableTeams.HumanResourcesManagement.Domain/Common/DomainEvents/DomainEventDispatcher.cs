namespace ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;

public class DomainEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(IDomainEvent @event, CancellationToken cancellationToken)
    {
        Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
        Type servicesList = typeof(IEnumerable<>).MakeGenericType(handlerType);
        IEnumerable<object> handlers = (IEnumerable<object>)_serviceProvider.GetService(servicesList)!;

        foreach (var handler in handlers)
        {
            IDomainEventHandler baseHandler = (handler as IDomainEventHandler)!;
            if (baseHandler != null)
            {
                await baseHandler.Handle(@event, cancellationToken);
            }
        }
    }
}