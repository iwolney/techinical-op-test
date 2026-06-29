using MassTransit;
using MongoDB.Driver;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels;
using TechnicalTestOpea.Contracts.Events;

namespace TechnicalTestOpea.Ports.DataReplicationConsumer.Consumers
{
    public sealed class LoanCreatedIntegrationEventConsumer(IMongoDatabase database) : IConsumer<LoanCreatedIntegrationEvent>
    {
        private readonly IMongoCollection<BookReadModel> collection = database.GetCollection<BookReadModel>("books");

        public async Task Consume(ConsumeContext<LoanCreatedIntegrationEvent> context)
        {
            var message = context.Message;

            var loan = new BookLoanReadModel
            {
                LoanId = message.LoanId,
                BorrowerName = message.BorrowerName,
                LoanDate = message.LoanDate,
                Status = message.Status
            };

            var update = Builders<BookReadModel>.Update
            .Set(x => x.QuantityAvailable, message.QuantityAvailable)
            .Push(x => x.ActiveLoans, loan);

            await collection.UpdateOneAsync(
                x => x.Id == message.BookId,
                update, 
                cancellationToken: context.CancellationToken);

        }
    }
}
