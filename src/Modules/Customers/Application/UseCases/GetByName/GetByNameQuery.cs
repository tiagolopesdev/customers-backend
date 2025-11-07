using Application.Shared.Dtos;
using MediatR;

namespace Application.UseCases.GetByName;

public sealed record class GetByNameQuery(string Name, string? UsersSales, DateTime? DateUsersSales, bool Owing) : IRequest<List<CustomerDto>>
{

}
