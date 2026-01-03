using BlockApplication.Contracts.Notification;
using BlockInfrastructure.EventBus;
using Customer.IntegrationEvent;

namespace Customer.Application.UseCases.UpdateStockProduct
{
    public class UpdateStockProductNotificationHandler : INotificationEventHandler<UpdateStockProductNotification>
    {
        public readonly IEventBus _eventBus;

        public UpdateStockProductNotificationHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(UpdateStockProductNotification notification, CancellationToken cancellationToken)
        {
            var products = new Dictionary<Guid, int>();

            notification.Buy.ForEach(buy =>
            {
                products.Add(buy.ProductId, buy.Quantity);
            });

            _eventBus.Publish(new UpdateStockModelntegrationEvent(products), "update-stock");

            return Task.CompletedTask;
        }
    }
}
