using Application.Shared.Dtos;
using BlockApplication.Contracts.CommandQuery;

namespace Application.UseCases.GetByName;

public sealed record class GetByNameQuery(string Name, string? UsersSales, DateTime? DateUsersSales, bool Owing) : IQuery<List<CustomerDto>>
{

}
