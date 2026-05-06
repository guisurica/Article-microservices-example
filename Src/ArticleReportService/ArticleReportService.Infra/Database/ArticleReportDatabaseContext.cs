using ArticleReportService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleReportService.Infra.Database;

public sealed class ArticleReportDatabaseContext : DbContext
{
    public DbSet<ArticleReport> ArticleReports { get; set; }

    public ArticleReportDatabaseContext(DbContextOptions<ArticleReportDatabaseContext> options) : base(options)
    {
    }

    
}
