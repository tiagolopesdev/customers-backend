using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.Payment
{
    public class PaymentEntity : Entity
    {
        public double Value { get; set; }

        public PaymentEntity(double value)
        {
            Id = Guid.NewGuid();
            Value = value;
            DateCreated = DateTime.Now;
        }
    }
}
