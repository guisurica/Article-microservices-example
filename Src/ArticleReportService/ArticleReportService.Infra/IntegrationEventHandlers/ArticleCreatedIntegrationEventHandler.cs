using ArticleReportService.Application.Contracts.IntegrationEvents;
using ArticleReportService.Infra.Database;
using ArticleReportService.Infra.IntegrationEvents;

namespace ArticleReportService.Infra.IntegrationEventHandlers;

public class ArticleCreatedIntegrationEventHandler : IIntegrationEventHandler<ArticleCreatedIntegrationEvent>
{
    private readonly ArticleReportDatabaseContext _context;

    public ArticleCreatedIntegrationEventHandler(ArticleReportDatabaseContext context)
    {
        _context = context;
    }

    public Task HandleAsync(ArticleCreatedIntegrationEvent @event)
    {
        throw new NotImplementedException();
    }
}
