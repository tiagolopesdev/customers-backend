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

        public static Product NewEntity(Product product)
        {
            product.Id = Guid.NewGuid();
            product.DateCreated = DateTime.Now;

            return product;
        }
    }
}
