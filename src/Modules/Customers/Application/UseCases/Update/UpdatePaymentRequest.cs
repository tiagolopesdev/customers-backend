using Domain.Customers;

namespace Customer.Application.UseCases.Update;

public class UpdatePaymentRequest : CustomerActionsResponse
{
  public double Value { get; set; }
  public PaymentMethod PaymentMethod { get; set; }
}
