using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Customer.Application.Configuration
{
    public static class DependencyInjection
    {
        public static void AddCustomerApplication(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
