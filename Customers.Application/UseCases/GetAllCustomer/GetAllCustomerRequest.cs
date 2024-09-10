
using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.GetAllCustomer
{
    public sealed record class GetAllCustomerRequest : IRequest<List<CustomerDTO>>
    {
    }
}
