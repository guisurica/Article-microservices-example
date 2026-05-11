namespace ArticleReportService.Application.Contracts.IntegrationEvents;

public interface IIntegrationEventDispatcher
{
    Task DispatchAsync(IIntegrationEvent integrationEvent);
}
