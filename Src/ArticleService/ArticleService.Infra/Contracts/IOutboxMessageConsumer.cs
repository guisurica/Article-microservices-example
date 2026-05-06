using RabbitMQ.Client;

namespace ArticleService.Infra.Contracts;

public interface IOutboxMessageConsumer
{
    Task ConsumeAsync();
    Task<int> CreateRabbitMqConnections(ConnectionFactory factory);
    Task<int> DisposeRabbitMqConnections();
}
