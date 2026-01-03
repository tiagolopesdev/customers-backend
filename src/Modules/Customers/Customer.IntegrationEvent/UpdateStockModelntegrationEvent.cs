
namespace Customer.IntegrationEvent
{
    public class UpdateStockModelntegrationEvent : BlockInfrastructure.EventBus.IntegrationEvent
    {
        public Dictionary<Guid, int> Products { get; set; }
       
        public UpdateStockModelntegrationEvent(Dictionary<Guid, int> products) : base()
        {
            Products = products;
        }
    }
}
