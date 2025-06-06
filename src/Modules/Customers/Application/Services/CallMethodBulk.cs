using Domain.Customers;

namespace Application.Services;

public class CallMethodBulk
{
  public static List<Customer> AssignAmountToPayList(List<Customer> customers)
  {
    customers.ForEach(item =>
    {
      item = Customer.AssignAmountToPay(item);
    });
    return customers;
  }
}
