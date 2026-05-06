using ArticleReportService.Domain.Entities;
using SharedService.Returns;

namespace ArticleReportService.Infra.Repositories;

public interface IArticleReportRepository
{
    public Task<Result<string>> CreateArticleReportAsync(ArticleReport entity);
}