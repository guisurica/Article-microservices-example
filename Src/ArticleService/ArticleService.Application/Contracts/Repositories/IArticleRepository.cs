using ArticleService.Domain.Entities;
using SharedService.Returns;

namespace ArticleService.Application.Contracts.Repositories;

public interface IArticleRepository
{
    Task<Result<Article>> GetArticleAsync(string id);
    Task<Result<string>> CreateArticleAsync(Article article);

}
