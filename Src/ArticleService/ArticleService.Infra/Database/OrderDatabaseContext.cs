using Microsoft.EntityFrameworkCore;

namespace ArticleService.Infra.Database;

public class OrderDatabaseContext : DbContext
{
    public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options) : base(options) { }
}
