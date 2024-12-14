using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.ValidatePayment
{
    public sealed record class ValidatePaymentRequest : IRequest<bool>
    {
        public Guid CustomerId { get; set; }
        public double Value { get; set; }
    }
}
