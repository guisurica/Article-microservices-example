using ArticleReportService.Domain.Entities;
using ArticleReportService.Infra.Database;
using SharedService.Returns;

namespace ArticleReportService.Infra.Repositories;

public sealed class ArticleReportRepository : IArticleReportRepository
{
    private readonly ArticleReportDatabaseContext _context;

    public ArticleReportRepository(ArticleReportDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> CreateArticleReportAsync(ArticleReport entity)
    {
        try
        {
            await _context.AddAsync(entity);

            await _context.SaveChangesAsync();

            return Result<string>.SuccessResult("Article report created successfully.", 201, entity.Id.ToString());
        } catch (Exception ex)
        {
            return Result<string>.FailureResult("Something bad happened, please try again later", 500);
        }
    }
}
