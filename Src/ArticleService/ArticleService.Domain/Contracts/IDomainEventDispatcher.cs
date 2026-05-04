namespace ArticleService.Domain.Contracts;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent);
}
