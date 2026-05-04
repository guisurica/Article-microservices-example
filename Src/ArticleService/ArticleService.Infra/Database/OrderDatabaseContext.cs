using ArticleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ArticleService.Infra.Database;

public class OrderDatabaseContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Outbox> Outboxes { get; set; }

    public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Outbox>(outbox =>
        {
            outbox.Property(outbox => outbox.Content)
                .HasColumnType("jsonb");

        });
    }
}
