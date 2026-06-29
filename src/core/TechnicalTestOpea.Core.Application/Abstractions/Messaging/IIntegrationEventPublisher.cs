namespace TechnicalTestOpea.Core.Application.Abstractions.Messaging
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}
