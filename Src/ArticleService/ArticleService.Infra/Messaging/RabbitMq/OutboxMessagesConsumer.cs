using ArticleService.Domain.Entities;
using ArticleService.Infra.Configurations;
using ArticleService.Infra.Contracts;
using ArticleService.Infra.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace ArticleService.Infra.Messaging.RabbitMq;
public class OutboxMessagesConsumer : IOutboxMessageConsumer
{
    private readonly OrderDatabaseContext _context;
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;

    private IConnection _connection;
    private IChannel _channel;
    private ConnectionFactory factory;

    private string DirectExchangeName = "direct_outbox_messages";

    public OutboxMessagesConsumer(OrderDatabaseContext context, IOptions<RabbitMqConfiguration> options)
    {
        _context = context;
        _rabbitMqConfiguration = options.Value;

        factory = new ConnectionFactory 
        { 
            HostName = _rabbitMqConfiguration.Host,
            UserName = _rabbitMqConfiguration.Username,
            Password = _rabbitMqConfiguration.Password,
        };

    }


    private async Task<int> CreateRabbitMqConnections(ConnectionFactory factory)
    {
        try
        {
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            return 0;
        } catch (Exception)
        {
            return 1;
        }
    }

    private async Task<int> DisposeRabbitMqConnections()
    {
        try
        {
            await _connection.DisposeAsync();
            await _channel.DisposeAsync();

            return 0;
        }
        catch (Exception)
        {
            return 1;
        }
    }


    public async Task ConsumeAsync()
    {
        List<Outbox> OutboxMessages = new List<Outbox>();

        OutboxMessages = await _context.Set<Outbox>()
            .AsNoTracking()
            .Where(outbox => outbox.ProcessedOnUtc == null)
            .ToListAsync();

        if (OutboxMessages.Count() <= 0)
            return;
        
        if (await CreateRabbitMqConnections(factory) == 1)
            return;

        await _channel
            .ExchangeDeclareAsync(exchange: DirectExchangeName, type: ExchangeType.Direct);
        
        foreach(Outbox message in OutboxMessages)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await _channel
                .BasicPublishAsync(exchange: DirectExchangeName, routingKey: DirectExchangeName, body: body);

            //message.UpdateProcessedOnUtc();
            
            //_context.Set<Outbox>().Update(message);

            //await _context.SaveChangesAsync();
        }

        await DisposeRabbitMqConnections();
    }
}
