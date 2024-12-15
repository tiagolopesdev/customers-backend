using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double BasePrice { get; set; }
        public int Quantity { get; set; }

        public static Product NewEntity(string name, string description, double value, double basePrice, int quantity)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                Name = name,
                Description = description,
                Value = value,
                BasePrice = basePrice,
                Quantity = quantity
            };

            return product;
        }
    }
}
