
using Customers.Domain.AggregatesModel.CustomerAggregate;

namespace Customers.Application.Shared.DTO
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PaymentDTO>? Payments { get; set; }
        public List<BuyDTO>? Buys { get; set; }
        public double AmountPaid { get; set; }
        public double AmountToPay { get; set; }
        public DateTime? DateCreated { get; set; }        
    }
}
