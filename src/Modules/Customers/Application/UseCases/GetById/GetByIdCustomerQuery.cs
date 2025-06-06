using Application.Contracts.Query;
using Application.Shared.Dtos;

namespace Application.UseCases.GetById;

public sealed record class GetByIdCustomerQuery(Guid Id) : IQuery<CustomerDto>
{
}
