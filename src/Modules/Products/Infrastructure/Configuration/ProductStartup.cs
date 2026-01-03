using BlockApplication.Behavior;
using BlockInfrastructure.EventBus;
using BlockInfrastructure.Startup;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Configuration;
using Product.Domain.Product;
using Product.Infrastructure.Persistence.Repositories;
using System.Reflection;

namespace Product.Infrastructure.Configuration
{
    public static class ProductStartup
    {
        public static void LoadProductModule(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddProductApplication();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddRabbitMQSubscription<>();

            StartMessageBroker(services);
        }

        public static void StartMessageBroker(this IServiceCollection services)
        {
            services.AddBlockInfrastructure();

            ConsumerTest.CreateConsumer();
        }
    }

    public class ConsumerTest
    {
        public static void CreateConsumer()
        {
            var consumer = new EventBusClient(); //.StartConsuming("update-stock");

            consumer.Subscribe();
            consumer.StartConsuming("update-stock");
        }
    }
}
