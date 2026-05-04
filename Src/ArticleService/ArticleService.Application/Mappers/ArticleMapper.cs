using ArticleService.Application.DTOs;
using ArticleService.Domain.Entities;

namespace ArticleService.Application.Mappers;

public static class ArticleMapper
{
    public static ArticleDTO MapToDTO(Article origin)
    {
        return new ArticleDTO { };
    }
}
