using ArticleService.Infra.Database;
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

        return service;
    }
}
