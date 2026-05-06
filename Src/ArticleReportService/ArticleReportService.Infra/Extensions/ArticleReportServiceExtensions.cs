using ArticleReportService.Infra.Configs;
using ArticleReportService.Infra.Database;
using ArticleReportService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleReportService.Infra.Extensions;

public static class ArticleReportServiceExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<RabbitMqConfiguration>(opt => {
            opt.Host = configuration["RabbitMqConfiguration:Host"];
            opt.Username = configuration["RabbitMqConfiguration:Username"];
            opt.Password = configuration["RabbitMqConfiguration:Password"];
        });

        return service;
    }
}
