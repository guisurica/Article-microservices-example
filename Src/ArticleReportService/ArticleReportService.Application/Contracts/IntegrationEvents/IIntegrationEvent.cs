namespace ArticleReportService.Application.Contracts.IntegrationEvents;

public interface IIntegrationEvent
{
    public Guid Id { get; }
    public DateTime OccurredOnUtc { get; }
    public string Type { get; }
    public string Content { get; }
}
