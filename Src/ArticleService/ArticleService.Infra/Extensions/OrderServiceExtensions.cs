using ArticleService.Application.Contracts.Repositories;
using SharedService.Contracts;
using ArticleService.Domain.Events;
using ArticleService.Infra.Configurations;
using ArticleService.Infra.Contracts;
using ArticleService.Infra.Database;
using ArticleService.Infra.EventHandlers;
using ArticleService.Infra.Messaging.RabbitMq;
using ArticleService.Infra.Repositories;
using ArticleService.Infra.Workers;
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
        service.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        service.AddScoped<IDomainEventHandler<ArticleCreatedDomainEvent>, ArticleCreatedEventHandler>();
        service.AddTransient<IOutboxMessageConsumer, OutboxMessagesConsumer>();

        service.AddHostedService<IntegrationEventWorker>();

        service.Configure<RabbitMqConfiguration>(opt => {
            opt.Host = configuration["RabbitMqConfiguration:Host"];
            opt.Username = configuration["RabbitMqConfiguration:Username"];
            opt.Password = configuration["RabbitMqConfiguration:Password"];
        });

        return service;
    }
}
