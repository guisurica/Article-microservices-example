using ArticleService.Infra.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArticleService.Infra.Workers;

public class OutboxMessageWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OutboxMessageWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using var scopedServices = _serviceProvider.CreateScope();

            var consumer = scopedServices.ServiceProvider.GetRequiredService<IOutboxMessageConsumer>();

            if (consumer is null)
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                await consumer.ConsumeAsync();

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        } catch (Exception ex)
        {

        }
    }
}
