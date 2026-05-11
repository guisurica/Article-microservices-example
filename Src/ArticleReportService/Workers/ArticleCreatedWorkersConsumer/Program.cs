using ArticleCreatedWorkersConsumer;
using ArticleReportService.Application.Contracts.IntegrationEvents;
using ArticleReportService.Infra.Configs;
using ArticleReportService.Infra.Messaging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IIntegrationEventDispatcher, IntegrationEventDispatcher>();

builder.Services.Configure<RabbitMqConfiguration>(opt => {
    opt.Host = builder.Configuration["RabbitMqConfiguration:Host"];
    opt.Username = builder.Configuration["RabbitMqConfiguration:Username"];
    opt.Password = builder.Configuration["RabbitMqConfiguration:Password"];
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
