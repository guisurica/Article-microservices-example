using ArticleReportService.Application.Contracts.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;
using SharedService.Contracts;

namespace ArticleReportService.Infra.Messaging;

public class IntegrationEventDispatcher : IIntegrationEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public IntegrationEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IIntegrationEvent integrationEvent)
    {
        var handlerType = typeof(IIntegrationEventHandler<>)
            .MakeGenericType(integrationEvent.GetType());

        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            await ((dynamic)handler).HandleAsync((dynamic)integrationEvent);
        }
    }
}
