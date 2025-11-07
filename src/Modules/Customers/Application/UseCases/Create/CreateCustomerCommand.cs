
using MediatR;

namespace Application.UseCases.Create;

public sealed record class CreateCustomerCommand(
  string Name,
  List<CreatePaymentCommand> Payments,
  List<CreateBuyCommand> Buys
  ) : IRequest<Guid>
{
  public Guid Id { get; set; }
}
