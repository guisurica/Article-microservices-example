using ArticleService.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Infra.Contracts;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _provider;

    public DomainEventDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent)
    {
        var handlerType = typeof(IDomainEventHandler<>)
            .MakeGenericType(domainEvent.GetType());

        var handlers = _provider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            await ((dynamic)handler).HandleAsync((dynamic)domainEvent);
        }
    }
}
