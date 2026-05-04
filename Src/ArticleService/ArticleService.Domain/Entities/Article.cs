using ArticleService.Domain.Contracts;
using SharedService.Returns;

namespace ArticleService.Domain.Entities;

public sealed class Article : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<string> Tags { get; private set; }

    public Article() { }

    private Article(string title, string description, List<string>? tags)
    {
        Title = title;
        Description = description;
        Tags = tags.Count() <= 0 ? new List<string> { "Default" } : tags;
    }

    public static Result<Article> Builder(string title, string description, List<string>? tags)
    {
        return Result<Article>.SuccessResult("Article created", new Article(title, description, tags));
    }
}
