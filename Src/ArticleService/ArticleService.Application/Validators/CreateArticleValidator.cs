using ArticleService.Application.DTOs;
using FluentValidation;

namespace ArticleService.Application.Validators;

public class CreateArticleValidator : AbstractValidator<CreateArticleDTO>
{
    public CreateArticleValidator()
    {
        RuleFor(article => article.Title)
            .MinimumLength(4)
            .WithMessage("The article title must have more than 4 characters");

        RuleFor(article => article.Title)
            .MaximumLength(255)
            .WithMessage("The article title must not have more than 255 characters");

        RuleFor(article => article.Title)
            .NotNull()
            .WithMessage("The article title must have a title");

        RuleFor(article => article.Description)
            .MinimumLength(4)
            .WithMessage("The article description must have more than 4 characters");

    }
}
