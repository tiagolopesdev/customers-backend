using BlockApplication.Helpers;
using Domain;

namespace Application.Services;

public static class PrecisionValues
{
  public static Customer PrecisionDecimalValues(Customer customer)
  {
    if (customer.Buys != null && customer.Buys.Count > 0)
    {
      customer.Buys.ForEach(item =>
      {
        item.Price = CommonHelpers.CalculatePrecision(item.Price);
        item.Total = CommonHelpers.CalculatePrecision(item.Total);
      });
    }

    if (customer.Payments != null && customer.Payments.Count > 0)
    {
      customer.Payments.ForEach(item =>
      {
        item.Value = CommonHelpers.CalculatePrecision(item.Value);
      });
    }

    return customer;
  }
  // public static List<Customer> AssignAmountToPayList(List<Customer> customers)
  // {
  //   customers.ForEach(item =>
  //   {
  //     item = Customer.AssignAmountToPay(item);
  //   });

  //   return customers;
  // }
}
