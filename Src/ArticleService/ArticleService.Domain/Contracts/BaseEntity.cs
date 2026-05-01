namespace ArticleService.Domain.Contracts;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? UpdatedOnUtc { get; private set; } = null;

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
}
