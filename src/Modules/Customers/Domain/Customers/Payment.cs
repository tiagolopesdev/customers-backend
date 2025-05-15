using BlockDomain.SeedWork;

namespace Domain.Customers;

public class Payment : Entity
{
  public double Value { get; set; }
  public PaymentMethod PaymentMethod { get; set; }

  public Payment(double value)
  {
    Value = value;
  }

  public static Payment NewEntity(Payment payment)
  {
    payment.Id = Guid.NewGuid();
    payment.DateCreated = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time"));

    return payment;
  }
}
