namespace ArticleReportService.Application.Contracts.IntegrationEvents;

public interface IIntegrationEventHandler<THandler> where THandler : IIntegrationEvent
{
    public Task HandleAsync(THandler @event);
}
