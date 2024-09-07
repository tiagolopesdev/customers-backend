
using Customers.Domain.Interfaces;
using Customers.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
