using ArticleService.Application.Contracts;
using ArticleService.Infra.Database;
using ArticleService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Infra.Extensions;

public static class OrderServiceExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<OrderDatabaseContext>(opt => 
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        service.AddTransient<IArticleRepository, ArticleRepository>();

        return service;
    }
}
