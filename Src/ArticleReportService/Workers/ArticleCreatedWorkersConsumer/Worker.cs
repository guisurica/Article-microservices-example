using ArticleReportService.Infra.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ArticleCreatedWorkersConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;
    public Worker(ILogger<Worker> logger,
        IOptions<RabbitMqConfiguration> options)
    {
        _logger = logger;
        _rabbitMqConfiguration = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() 
        { 
            HostName = _rabbitMqConfiguration.Host,
            UserName = _rabbitMqConfiguration.Username,
            Password = _rabbitMqConfiguration.Password,
        };

        using var _connection = await factory.CreateConnectionAsync(stoppingToken);
        using var _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.QueueDeclareAsync(queue: "ArticleCreatedDomainEvent", exclusive: false, durable: true, autoDelete: false,
             arguments: new Dictionary<string, object?> { { "x-queue-type", "quorum" } }, cancellationToken: stoppingToken);

        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) => 
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    
                } catch (Exception)
                {

                }
            };

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
