using ArticleService.Domain.Entities;
using SharedService.Returns;

namespace ArticleService.Application.Contracts;

public interface IArticleRepository
{
    Task<Result<Article>> GetArticleAsync(string id);
    Task<Result<string>> CreateArticleAsync(Article article);

}
