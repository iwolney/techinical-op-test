using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Author)
                .HasColumnName("Author")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.PublicationYear)
                .HasColumnName("PublicationYear")
                .IsRequired();

            builder.Property(x => x.QuantityAvailable)
                .HasColumnName("QuantityAvailable")
                .IsRequired();
        }
    }
}
