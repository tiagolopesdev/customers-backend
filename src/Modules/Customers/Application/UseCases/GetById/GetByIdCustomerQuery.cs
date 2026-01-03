using BlockApplication.Contracts.CommandQuery;
using Customer.Application.Shared.Dtos;

namespace Customer.Application.UseCases.GetById;

public sealed record class GetByIdCustomerQuery(Guid Id) : IQuery<CustomerDto>
{
}
