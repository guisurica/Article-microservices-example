using ArticleService.Domain.Contracts;

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

    public static Article Builder(string title, string description, List<string>? tags)
    {
        return new Article(title, description, tags);
    }
}
