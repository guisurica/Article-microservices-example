namespace ArticleService.Domain.Contracts;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? UpdatedOnUtc { get; private set; } = null;

    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public bool IsDeleted { get; private set; } = false;

    public BaseEntity()
    {
        Id = Guid.NewGuid();

        CreatedOnUtc = DateTime.UtcNow;
    }

    protected void UpdateEntity()
    {
        UpdatedOnUtc = DateTime.UtcNow;
    }

    protected void DeleteEntity()
    {
        IsDeleted = true;
    }

    public void RaiseDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public List<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents;
    }
}
