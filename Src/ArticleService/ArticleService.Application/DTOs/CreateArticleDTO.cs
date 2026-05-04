namespace ArticleService.Application.DTOs;

public record CreateArticleDTO(
        string Title,
        string Description, 
        List<string>? Tags
    );