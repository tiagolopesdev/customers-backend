using Application.Configuration;
using BlockApplication.Behavior;
using Domain.Product;
using FluentValidation;
using Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Configuration
{
    public static class ProductStartup
    {
        public static void LoadProductModule(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            DependencyInjection.AddProductApplication(services);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
