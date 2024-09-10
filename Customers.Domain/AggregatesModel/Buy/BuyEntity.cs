using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.Buy
{
    public class BuyEntity : Entity
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total {  get; set; }

        public BuyEntity(double price, int quantity)
        {
            Id = Guid.NewGuid();
            Price = price;
            Quantity = quantity;
            Total = price * quantity;
            DateCreated = DateTime.Now;
        }
    }
}
