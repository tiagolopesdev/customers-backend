using BlockDomain.SeedWork;

namespace Domain;

public class Customer : Entity, IAggregateRoot
{
  public string Name { get; set; }
  public List<Payment>? Payments { get; set; }
  public List<Buy>? Buys { get; set; }
  public double AmountPaid { get; set; }
  public double AmountToPay { get; set; }

  public Customer()
  {
  }

  public static Customer NewEntity(Customer customer)
  {
    customer.Id = Guid.NewGuid();
    customer.DateCreated = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time"));

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
  public Customer UpdateAmountToPay(Customer customer) 
  {
    if (customer.Payments != null && customer.Payments.Count > 0) 
    {
      customer.AmountToPay -= customer.Payments.Sum(payment => payment.Value);
    }
    return customer;
  }
  public static Customer AssignAmountToPay(Customer customer)
  {
    if (customer.Payments != null && customer.Payments.Count > 0)
    {
      customer.AmountToPay -= customer.Payments.Sum(payment => payment.Value);
    }

    return customer;
  }

}
