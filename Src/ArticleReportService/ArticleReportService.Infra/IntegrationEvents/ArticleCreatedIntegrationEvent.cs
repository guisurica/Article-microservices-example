using ArticleReportService.Application.Contracts.IntegrationEvents;

namespace ArticleReportService.Infra.IntegrationEvents;

public class ArticleCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid Id  { get; set; }

    public DateTime OccurredOnUtc  { get; set; }

    public string Type  { get; set; }

    public string Content  { get; set; }
}
