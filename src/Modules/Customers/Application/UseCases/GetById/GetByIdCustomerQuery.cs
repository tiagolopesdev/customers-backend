using Application.Shared.Dtos;
using BlockApplication.Contracts.CommandQuery;

namespace Application.UseCases.GetById;

public sealed record class GetByIdCustomerQuery(Guid Id) : IQuery<CustomerDto>
{
}
