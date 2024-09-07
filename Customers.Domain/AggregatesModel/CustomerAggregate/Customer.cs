

using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.Payment;
using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public List<PaymentEntity>? Payment { get; set; }
        public List<BuyEntity>? Buy { get; set; }
        private double AmountPaid { get; set; }
        private double AmountToPay { get; set; }

        public Customer(string name, List<PaymentEntity>? payment, List<BuyEntity>? buy)
        {
            Id = Guid.NewGuid();
            Name = name;
            Payment = payment;
            Buy = buy;
            DateCreated = DateTime.Now;
            SetAmountPaid();
            SetAmountToPay();
        }

        public void SetAmountPaid()
        {
            if (Payment != null && Payment.Count != 0)
            {
                foreach (var item in Payment)
                {
                    AmountPaid += item.Value;
                }
            }
        }
        public void SetAmountToPay()
        {
            if (Buy != null && Buy.Count != 0)
            {
                foreach (var item in Buy)
                {
                    AmountToPay += item.Total;
                }
            }
        }
    }
}
