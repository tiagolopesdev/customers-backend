using Application.Contracts.Query;
using Application.Shared.Dtos;

namespace Application.UseCases.GetByName;

public sealed record class GetByNameQuery(string Name, string? UsersSales, DateTime? DateUsersSales, bool Owing) : IQuery<List<CustomerDto>>
{

}
