using BlockApplication.Contracts.CommandQuery;
using Customer.Application.Shared.Dtos;

namespace Customer.Application.UseCases.GetByName;

public sealed record class GetByNameQuery(string Name, string? UsersSales, DateTime? DateUsersSales, bool Owing) : IQuery<List<CustomerDto>>
{

}
