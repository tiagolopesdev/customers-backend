using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Product.Application.Configuration
{
    public static class DependencyInjection
    {
        public static void AddProductApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));  
        }

        //public static void ConfigureMessageBroker
    }
}
