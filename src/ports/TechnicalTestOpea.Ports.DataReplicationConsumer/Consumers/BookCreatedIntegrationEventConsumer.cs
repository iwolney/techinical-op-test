using MassTransit;
using MongoDB.Driver;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels;
using TechnicalTestOpea.Contracts.Events;

namespace TechnicalTestOpea.Ports.DataReplicationConsumer.Consumers
{
    public sealed class BookCreatedIntegrationEventConsumer(IMongoDatabase database) : IConsumer<BookCreatedIntegrationEvent>
    {
        private readonly IMongoCollection<BookReadModel> collection = database.GetCollection<BookReadModel>("books");

        public async Task Consume(
            ConsumeContext<BookCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            var model = new BookReadModel
            {
                Id = message.BookId,
                Title = message.Title,
                Author = message.Author,
                PublicationYear = message.PublicationYear,
                QuantityAvailable = message.QuantityAvailable
            };

            await collection.ReplaceOneAsync(
                x => x.Id == model.Id,
                model,
                new ReplaceOptions { IsUpsert = true },
                context.CancellationToken);
        }
    }
}
