
namespace BlockInfrastructure.EventBus
{
    public interface IEventBus
    {
        void Publish<T>(T objectPublish) where T : IntegrationEvent;
        void Subscribe<T>(T objectSubscribe) where T : IntegrationEvent;
        void StartConsuming();
    }
}
