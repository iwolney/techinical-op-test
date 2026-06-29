namespace TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels
{
    public sealed class BookLoanReadModel
    {
        public Guid LoanId { get; set; }
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
