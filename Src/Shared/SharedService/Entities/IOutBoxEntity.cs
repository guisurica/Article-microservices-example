using Microsoft.VisualBasic;

namespace SharedService.Entities;

public abstract class OutBoxEntity 
{ 
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    
    public OutBoxEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}

