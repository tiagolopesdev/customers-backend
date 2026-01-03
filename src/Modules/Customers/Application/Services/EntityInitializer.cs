using BlockApplication.Helpers;
using BlockDomain.SeedWork;
using Domain.Customers;

namespace Customer.Application.Services;

public class EntityInitializer
{
    public static void DefineNewOrUpdateEntity<T>(List<T> entityList) where T : Entity
    {
        entityList.ForEach(item =>
        {
            if (item.Id == null || item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
                item.DateCreated = CommonHelpers.DateTimeForBrazil();
            }
            else
            {
                item.DateUpdated = CommonHelpers.DateTimeForBrazil();
            }
        });
    }

    public static CustomerAggregateRoot DefineBuyEqualsNew(CustomerAggregateRoot customer)
    {
        var paymentList = new List<Buy>();

        if (customer.Buys != null && customer.Buys.Count > 0)
        {
            var paymentForAssign = new Buy(0.0, 0, "");

            foreach (var item in customer.Buys)
            {
                paymentForAssign = Buy.NewEntity(item);
                paymentForAssign = item;
            }
            paymentList.Add(paymentForAssign);
        }

        return customer;
    }

    public static CustomerAggregateRoot DefinePaymentEqualsNew(CustomerAggregateRoot customer)
    {
        var paymentList = new List<Payment>();

        if (customer.Payments != null && customer.Payments.Count > 0)
        {
            var paymentForAssign = new Payment(0.0);

            foreach (var item in customer.Payments)
            {
                paymentForAssign = Payment.NewEntity(item);
                paymentForAssign = item;
            }
            paymentList.Add(paymentForAssign);
        }

        return customer;
    }
}
