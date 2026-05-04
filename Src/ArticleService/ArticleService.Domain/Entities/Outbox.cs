using SharedService.Entities;

namespace ArticleService.Domain.Entities;

public sealed class Outbox : OutBoxEntity
{
    public DateTime? ProcessedOnUtc { get; private set; } = null;
    public string Content { get; private set; }
    public int NumberOfRetries { get; private set; }

    public Outbox(string content) 
    {
        Content = content;
    }

    public static Outbox Build(string content)
    {
        return new Outbox(content);
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
