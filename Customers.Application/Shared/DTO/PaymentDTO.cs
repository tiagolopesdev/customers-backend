
namespace Customers.Application.Shared.DTO
{
    public class PaymentDTO
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
