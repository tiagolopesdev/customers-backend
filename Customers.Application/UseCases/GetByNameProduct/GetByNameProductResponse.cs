
namespace Customers.Application.UseCases.GetByNameProduct
{
    public class GetByNameProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
