using Customer.Application.Configuration;
using Domain.Customers;
using FluentValidation;
using Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Customer.Infrastructure.Configuration
{
    public static class CustomerStartup
    {
        public static void LoadCustomerModule(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            DependencyInjection.AddCustomerApplication(services);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BlockApplication.Behavior.ValidationBehavior<,>));                        
        }        
    }
}
