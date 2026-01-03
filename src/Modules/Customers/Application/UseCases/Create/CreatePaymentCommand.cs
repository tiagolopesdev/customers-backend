namespace Customer.Application.UseCases.Create;

public class CreatePaymentCommand
{
  public double Value { get; set; }
  // public PaymentMethod PaymentMethod { get; set; }
  public string UpdatedBy { get; set; }
}
