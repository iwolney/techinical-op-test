using Microsoft.EntityFrameworkCore;
using TechnicalTestOpea.Core.Domain.Entities;
using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) 
        { 
        }

        #region Set

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Loan> Loans => Set<Loan>();

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);

            modelBuilder.Ignore<DomainEvent>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
