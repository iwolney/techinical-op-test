using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Context;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Dispatchers;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Repositories;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSqlServerInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlServer"),
                    sql =>
                    {
                        sql.MigrationsAssembly(
                            typeof(LibraryDbContext).Assembly.FullName);
                    });
            });

            return services;
        }
    }
}
