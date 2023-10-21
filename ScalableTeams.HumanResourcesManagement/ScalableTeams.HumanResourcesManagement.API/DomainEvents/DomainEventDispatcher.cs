using Microsoft.Extensions.DependencyInjection;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.DomainEvents;

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
        IEnumerable<object> handlers = _serviceProvider.GetServices(handlerType)!;

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