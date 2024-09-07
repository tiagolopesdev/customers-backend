
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.Payment;
using MediatR;

namespace Customers.Application.UseCases.CreateUser
{
    public sealed record class CreateCustomerRequest(string Name, List<PaymentEntity> Payments, List<BuyEntity> Buys) : IRequest<Guid>
    {
    }
}