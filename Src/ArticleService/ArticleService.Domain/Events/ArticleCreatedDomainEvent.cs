using ArticleService.Domain.Contracts;
using ArticleService.Domain.Entities;

namespace ArticleService.Domain.Events;

public class ArticleCreatedDomainEvent : IDomainEvent
{
    public Guid Id { get; set; }
    public DateTime OcorredOnUtc { get; set; }
    public string Content { get; set; }
}
