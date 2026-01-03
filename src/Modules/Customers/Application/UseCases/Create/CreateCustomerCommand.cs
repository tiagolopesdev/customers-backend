using BlockApplication.Contracts.CommandQuery;

namespace Customer.Application.UseCases.Create;

public sealed record class CreateCustomerCommand(
  string Name,
  List<CreatePaymentCommand> Payments,
  List<CreateBuyCommand> Buys
  ) : ICommand<Guid>
{
  public Guid Id { get; set; }
}
