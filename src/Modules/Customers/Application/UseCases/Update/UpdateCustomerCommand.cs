using Application.Contracts;

namespace Application.UseCases.Update;

public class UpdateCustomerComand
{
  public sealed record class UpdateCustomerCommand() : ICommand<Guid>
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<UpdatePaymentRequest> Payments { get; set; }
    public List<UpdateBuyRequest> Buys { get; set; }
    public bool IsEnable { get; set; }
  }
}
