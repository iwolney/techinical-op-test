using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTestOpea.Core.Domain.Entities;
using TechnicalTestOpea.Core.Domain.Enums;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BookId)
                .HasColumnName("BookId")
                .IsRequired();

            builder.Property(x => x.LoanDate)
                .HasColumnName("LoanDate")
                .IsRequired();

            builder.Property(x => x.BorrowerName)
                .HasColumnName("BorrowerName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ReturnDate)
                .HasColumnName("ReturnDate");

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasConversion(
                    status => status.ToString(),
                    value => Enum.Parse<LoanStatus>(value))
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
