using SharedService.Entities;

namespace ArticleService.Domain.Entities;

public sealed class Outbox : OutBoxEntity
{
    public DateTime? ProcessedOnUtc { get; private set; } = null;
    public string Content { get; private set; }
    public int NumberOfRetries { get; private set; }
    public string Type { get; private set; }

    public Outbox(string content, string type) 
    {
        Content = content;
        Type = type;
    }

    public static Outbox Build(string content, string type)
    {
        return new Outbox(content, type);
    }

    public void UpdateRetry()
    {
        this.NumberOfRetries += 1;
    }

    public void UpdateProcessedOnUtc()
    {
        this.ProcessedOnUtc = DateTime.UtcNow;
    }
}
