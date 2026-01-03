namespace Customer.Application.Shared.Dtos;

public class CustomerDto
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public List<PaymentDto>? Payments { get; set; }
  public List<BuyDto>? Buys { get; set; }
  public double AmountPaid { get; set; }
  public double AmountToPay { get; set; }
  public DateTime? DateCreated { get; set; }
  public string UpdatedBy { get; set; }
}
