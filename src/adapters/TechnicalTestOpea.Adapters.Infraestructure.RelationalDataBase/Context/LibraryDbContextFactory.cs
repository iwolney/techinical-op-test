using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Context;

public sealed class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
{
    public LibraryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=TechnicalTestOpeaDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;Encrypt=False",
            sql => sql.MigrationsAssembly(typeof(LibraryDbContext).Assembly.FullName));

        return new LibraryDbContext(optionsBuilder.Options);
    }
}