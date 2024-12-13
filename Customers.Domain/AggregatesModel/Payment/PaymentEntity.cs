using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.Payment
{
    public class PaymentEntity : Entity
    {
        public double Value { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public PaymentEntity(double value)
        {
            Value = value;
        }

        public static PaymentEntity NewEntity(PaymentEntity payment)
        {
            payment.Id = Guid.NewGuid();
            payment.DateCreated = DateTime.Now;

            return payment;
        }
    }
}
