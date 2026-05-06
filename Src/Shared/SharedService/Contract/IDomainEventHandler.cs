namespace SharedService.Contracts;

public interface IDomainEventHandler<THandler> where THandler : IDomainEvent
{
    Task HandleAsync(THandler domainEvent);
}
