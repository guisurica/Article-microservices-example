using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "ArticleCreatedDomainEvent", durable: true, exclusive: false, autoDelete: false, 
    arguments: new Dictionary<string, object?> { { "x-queue-type", "quorum" } });

await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine(message);
};

await channel.BasicConsumeAsync("ArticleCreatedDomainEvent", autoAck: true, consumer: consumer);

Console.ReadLine();
