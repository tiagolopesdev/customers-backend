using Application.Shared.Dtos;
using MediatR;

namespace Application.UseCases.GetById;

public sealed record class GetByIdCustomerQuery(Guid Id) : IRequest<CustomerDto>
{
}
