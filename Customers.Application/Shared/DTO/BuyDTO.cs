
namespace Customers.Application.Shared.DTO
{
    public class BuyDTO
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
