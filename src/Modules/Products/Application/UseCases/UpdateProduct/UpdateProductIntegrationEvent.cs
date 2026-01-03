using BlockInfrastructure.EventBus;

namespace Product.Application.UseCases.UpdateProduct
{
    public class UpdateProductIntegrationEvent 
    {
        public readonly IEventBus _eventBus;

        public UpdateProductIntegrationEvent(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Consumer()
        {
            _eventBus.StartConsuming("update-stock");
        }
    }
}
