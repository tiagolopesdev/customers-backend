using Domain.Customers;

namespace Application.Services;

public class CallMethodBulk
{
  public static List<CustomerAggregateRoot> AssignAmountToPayList(List<CustomerAggregateRoot> customers)
  {
    customers.ForEach(item =>
    {
      item = CustomerAggregateRoot.AssignAmountToPay(item);
    });
    return customers;
  }
}
