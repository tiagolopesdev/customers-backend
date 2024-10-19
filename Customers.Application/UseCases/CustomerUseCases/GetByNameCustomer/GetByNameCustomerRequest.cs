using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.GetByNameCustomer
{
    public sealed record class GetByNameCustomerRequest(string Name) : IRequest<List<CustomerDTO>>
    {
    }
}
