using SharedService.Entities;

namespace ArticleReportService.Domain.Entities;

public sealed class ArticleReport : BaseEntity
{
    public Guid ArticleId { get; private set; }
    public List<EventsInArticle> Events { get; private set; }
}

public sealed class EventsInArticle
{
    public string EventName { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }

    public EventsInArticle(string eventName, DateTime occurredOnUtc)
    {
        EventName = eventName;
        OccurredOnUtc = occurredOnUtc;
    }
}
