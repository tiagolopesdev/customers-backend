using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.Buy
{
    public class BuyEntity : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total {  get; set; }

        public BuyEntity(double price, int quantity, string name)
        {
            Price = price;
            Quantity = quantity;
            Total = price * quantity;
            Name = name;
        }

        public static BuyEntity NewEntity(BuyEntity buy)
        {
            buy.Id = Guid.NewGuid();
            buy.DateCreated = DateTime.Now;

            return buy;
        }
    }
}
