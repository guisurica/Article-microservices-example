using ArticleService.Application.Contracts.Usecases;
using ArticleService.Application.DTOs;
using ArticleService.Application.Usecases;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Domain.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddTransient<IUseCase<ArticleDTO, CreateArticleDTO>, CreateArticleUseCase>();

        return service;
    }
}
