
using Domain;

namespace Application.Services;

public class EntityNew
{
  public static Customer DefineBuyEqualsNew(Customer customer)
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

  public static Customer DefinePaymentEqualsNew(Customer customer)
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
