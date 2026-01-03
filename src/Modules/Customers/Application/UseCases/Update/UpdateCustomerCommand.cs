
using BlockApplication.Contracts.CommandQuery;

namespace Customer.Application.UseCases.Update;

public sealed record class UpdateCustomerCommand() : ICommand<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<UpdatePaymentRequest> Payments { get; set; }
    public List<UpdateBuyRequest> Buys { get; set; }
    public bool IsEnable { get; set; }
}
