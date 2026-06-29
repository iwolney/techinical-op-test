using MongoDB.Bson.Serialization.Attributes;

namespace TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels
{
    public sealed class BookReadModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int QuantityAvailable { get; set; }

        public List<BookLoanReadModel> ActiveLoans { get; set; } = [];
        public List<BookLoanReadModel> LoanHistory { get; set; } = [];
    }
}
