using MassTransit;
using TechnicalTestOpea.Core.Application.Abstractions.Messaging;

namespace TechnicalTestOpea.Adapters.Infraestructure.Messaging.Services
{
    public class MessageBroker(IPublishEndpoint publishEndpoint) : IIntegrationEventPublisher
    {
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
