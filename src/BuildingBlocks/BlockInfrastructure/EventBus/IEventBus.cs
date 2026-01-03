
namespace BlockInfrastructure.EventBus
{
    public interface IEventBus
    {
        void Publish<T>(T objectPublish, string queueName) where T : IntegrationEvent;
        void Subscribe();
        //void Subscribe<T>(T objectSubscribe) where T : IntegrationEvent;
        void StartConsuming(string queueName);
    }
}
