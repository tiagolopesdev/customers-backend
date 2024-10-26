using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.UpdateCustomer
{
    public abstract class CustomerActionsResponse
    {
        public Guid? Id { get; set; }
        public bool IsEnable { get; set; }
    }
    public sealed record class UpdateCustomerRequest() : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UpdatePaymentRequest> Payments { get; set; }
        public List<UpdateBuyRequest> Buys { get; set; }
        public bool IsEnable { get; set; }
    }

    public sealed class UpdateBuyRequest : CustomerActionsResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

    public sealed class UpdatePaymentRequest : CustomerActionsResponse
    {
        public double Value { get; set; }
    }
}
