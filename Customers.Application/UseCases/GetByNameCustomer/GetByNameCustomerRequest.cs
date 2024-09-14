
using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.GetByNameCustomer
{
    public sealed record class GetByNameCustomerRequest(string Name) : IRequest<List<CustomerDTO>>
    {
    }
}
