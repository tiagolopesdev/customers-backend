using BlockApplication.Contracts.CommandQuery;
using Customer.Application.Shared.Dtos;

namespace Customer.Application.UseCases.GetAll;

public class GetAllCustomerQuery : IQuery<List<CustomerDto>>
{
    public string? UsersSales { get; set; }
    public DateTime? DateUsersSales { get; set; }
    public bool Owing { get; set; }
}
