using ArticleService.Application.Contracts.Repositories;
using ArticleService.Domain.Contracts;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Events;
using ArticleService.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedService.Returns;
using System.Security.Principal;
using System.Text.Json;

namespace ArticleService.Infra.Repositories;

internal sealed class ArticleRepository : IArticleRepository
{
    private readonly OrderDatabaseContext _context;

    public ArticleRepository(OrderDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> CreateArticleAsync(Article article)
    {
        try
        {
            await _context.Set<Article>().AddAsync(article);

            article.RaiseDomainEvent(new ArticleCreatedDomainEvent
            {
                Id = article.Id,
                OcorredOnUtc = DateTime.UtcNow,
                Content = JsonSerializer.Serialize(article)
            });

            await _context.SaveChangesAsync();

            return Result<string>.SuccessResult("Article created successfully", 201);

        } catch (Exception)
        {
            return Result<string>.FailureResult("Something bad happened", 500);
        }
    }

    public async Task<Result<Article>> GetArticleAsync(string id)
    {
        try
        {
            var et = await _context
                .Set<Article>()
                .AsNoTracking()
                .FirstOrDefaultAsync(article => article.Id.ToString() == id);

            if (et is null)
                return Result<Article>.FailureResult("Article not found", 404);

            return Result<Article>.SuccessResult(string.Empty, 200, et);
        }
        catch (Exception)
        {
            return Result<Article>.FailureResult("Something bad happened", 500);
        }
    }
}
