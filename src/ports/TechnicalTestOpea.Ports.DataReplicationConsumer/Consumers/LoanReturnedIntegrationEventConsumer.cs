using MassTransit;
using MongoDB.Driver;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels;
using TechnicalTestOpea.Contracts.Events;

namespace TechnicalTestOpea.Ports.DataReplicationConsumer.Consumers
{
    public sealed class LoanReturnedIntegrationEventConsumer(IMongoDatabase database) : IConsumer<LoanReturnedIntegrationEvent>
    {
        private readonly IMongoCollection<BookReadModel> collection = database.GetCollection<BookReadModel>("books");

        public async Task Consume(ConsumeContext<LoanReturnedIntegrationEvent> context)
        {
            var message = context.Message;

            var book = await collection
                .Find(x => x.Id == message.BookId)
                .FirstOrDefaultAsync(context.CancellationToken);

            if (book is null)
                return;

            var loan = book.ActiveLoans
                .FirstOrDefault(x => x.LoanId == message.LoanId);

            if (loan is null)
                return;

            book.ActiveLoans.Remove(loan);

            loan.Status = message.Status;
            loan.ReturnDate = message.ReturnDate;

            book.LoanHistory.Add(loan);
            book.QuantityAvailable = message.QuantityAvailable;

            await collection.ReplaceOneAsync(x => x.Id == book.Id,
                book,
                cancellationToken: context.CancellationToken);
        }    
    }
}
