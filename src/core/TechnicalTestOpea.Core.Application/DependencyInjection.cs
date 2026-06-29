using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TechnicalTestOpea.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });

           
            return services;
        }
    }
}
