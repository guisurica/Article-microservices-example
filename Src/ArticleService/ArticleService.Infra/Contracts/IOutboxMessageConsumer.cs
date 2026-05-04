namespace ArticleService.Infra.Contracts;

public interface IOutboxMessageConsumer
{
    Task ConsumeAsync();
}
