
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.Payment;
using Customers.Domain.SeedWork;

namespace Customers.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public List<PaymentEntity>? Payments { get; set; }
        public List<BuyEntity>? Buys { get; set; }
        public double AmountPaid { get; set; }
        public double AmountToPay { get; set; }

        public Customer()
        {
        }

        public static Customer NewEntity(Customer customer)
        {
            customer.SetDateCreated();
            
            return customer;
        }
        public void SetAmountPaid()
        {
            if (Payments != null && Payments.Count > 0)
            {
                foreach (var item in Payments)
                {
                    if (item.DateDeleted != null) continue;

                    AmountPaid += item.Value;
                }
            }
        }
        public void SetAmountToPay()
        {
            if (Buys != null && Buys.Count > 0)
            {
                foreach (var item in Buys)
                {
                    if (item.DateDeleted != null) continue;

                    AmountToPay += item.Total;
                }
            }
        }
    }
}
