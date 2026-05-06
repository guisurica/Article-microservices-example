using ArticleService.Application.Contracts.Repositories;
using ArticleService.Application.Contracts.Usecases;
using ArticleService.Application.DTOs;
using ArticleService.Application.Mappers;
using ArticleService.Application.Validators;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Events;
using FluentValidation;
using FluentValidation.Results;
using SharedService.Contracts;
using SharedService.Returns;

namespace ArticleService.Application.Usecases;

internal sealed class CreateArticleUseCase : IUseCase<ArticleDTO, CreateArticleDTO>
{
    private readonly IArticleRepository _repository;
    private readonly IDomainEventDispatcher _dispatcher;
    
    public CreateArticleUseCase(IArticleRepository repository, IDomainEventDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
    }

    public async Task<Result<ArticleDTO>> HandleAsync(CreateArticleDTO param)
    {
        AbstractValidator<CreateArticleDTO> articleValidator = new CreateArticleValidator();
        ValidationResult validation = articleValidator.Validate(param);

        if (!validation.IsValid)
            return Result<ArticleDTO>.FailureResult(validation.Errors.First().ErrorMessage, 400);

        var article = Article.Builder(param.Title, param.Description, param.Tags);

        if (!article.IsOkResult)
            return Result<ArticleDTO>.FailureResult(article.Message, article.Code);

        var articleCreated = await _repository.CreateArticleAsync(article.Data);
        if (!articleCreated.IsOkResult)
            return Result<ArticleDTO>.FailureResult(article.Message, article.Code);

        foreach(var @event in article.Data.GetDomainEvents())
        {
            await _dispatcher.DispatchAsync(@event);
        }

        article.Data.ClearDomainEvents();

        var articleMapped = ArticleMapper.MapToDTO(article.Data);

        return Result<ArticleDTO>.SuccessResult(articleCreated.Message, articleCreated.Code, articleMapped);
    }
}
