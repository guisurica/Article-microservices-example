using ArticleService.Domain.Contracts;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Events;
using ArticleService.Infra.Database;

namespace ArticleService.Infra.EventHandlers;

public class ArticleCreatedEventHandler : IDomainEventHandler<ArticleCreatedDomainEvent>
{
    private readonly OrderDatabaseContext _context;

    public ArticleCreatedEventHandler(OrderDatabaseContext context)
    {
        _context = context;
    }

    public async Task HandleAsync(ArticleCreatedDomainEvent domainEvent)
    {
        var outbox = await _context.Set<Outbox>().AddAsync(new Outbox(domainEvent.Content));

        await _context.SaveChangesAsync();
    }
}
