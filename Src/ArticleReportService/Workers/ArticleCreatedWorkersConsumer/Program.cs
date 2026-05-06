using ArticleCreatedWorkersConsumer;
using ArticleReportService.Infra.Configs;
using ArticleReportService.Infra.Database;
using ArticleReportService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IArticleReportRepository, ArticleReportRepository>();
builder.Services.AddDbContext<ArticleReportDatabaseContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<RabbitMqConfiguration>(opt => {
    opt.Host = builder.Configuration["RabbitMqConfiguration:Host"];
    opt.Username = builder.Configuration["RabbitMqConfiguration:Username"];
    opt.Password = builder.Configuration["RabbitMqConfiguration:Password"];
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
