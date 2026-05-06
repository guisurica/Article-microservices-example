using ArticleService.Domain.Entities;
using SharedService.Contracts;

namespace ArticleService.Domain.Events;

public class ArticleCreatedDomainEvent : IDomainEvent
{
    public Guid Id { get; set; }
    public DateTime OcorredOnUtc { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }
}
