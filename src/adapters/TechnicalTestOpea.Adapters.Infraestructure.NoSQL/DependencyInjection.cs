using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL.Repositories;
using TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories;

namespace TechnicalTestOpea.Adapters.Infraestructure.NoSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNoSqlDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["MongoDbSettings:ConnectionString"];
            var databaseName = configuration["MongoDbSettings:DatabaseName"];

            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);


            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddSingleton(database);

            services.AddScoped<IBookReadRepository, BookReadRepository>();
            services.AddScoped<ILoanReadRepository, LoanReadRepository>();

            return services;
        }
    }
}
