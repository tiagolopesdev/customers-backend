using BlockApplication.Contracts.Notification;
using BlockInfrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace BlockInfrastructure.Startup
{
    public static class BuildingBlockStartup
    {
        public static void LoadBuildingBlock(this IServiceCollection services)
        {
            services.AddBlockApplication();
            services.AddBlockInfrastructure();
        }
        private static void AddBlockApplication(this IServiceCollection services)
        {
            services.AddScoped<ISendNotification, SendNotification>();
        }
        public static void AddBlockInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEventBus, EventBusClient>();
        }
    }
}
