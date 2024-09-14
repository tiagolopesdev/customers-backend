
using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.GetByIdCustomer
{
    public sealed record class GetByIdCustomerRequest(Guid Id) : IRequest<CustomerDTO>
    {
    }
}
