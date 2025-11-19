
using Application.Shared.Dtos;
using BlockApplication.Contracts.CommandQuery;

namespace Application.UseCases.GetAll;

public class GetAllCustomerQuery : IQuery<List<CustomerDto>>
{
    public string? UsersSales { get; set; }
    public DateTime? DateUsersSales { get; set; }
    public bool Owing { get; set; }
}
