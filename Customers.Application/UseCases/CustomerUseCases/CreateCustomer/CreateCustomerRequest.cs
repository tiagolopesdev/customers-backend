using Customers.Domain.AggregatesModel.Payment;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.CreateCustomer
{
    public sealed record class CreateCustomerRequest(string Name, List<CreatePaymentRequest> Payments, List<CreateBuyRequest> Buys) : IRequest<Guid>
    {
    }

    public sealed class CreateBuyRequest
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string UpdatedBy { get; set; }
    }

    public sealed class CreatePaymentRequest
    {
        public double Value { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string UpdatedBy { get; set; }
    }
}