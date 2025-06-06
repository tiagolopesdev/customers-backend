
using Application.Contracts.Query;
using Application.Shared.Dtos;

namespace Application.UseCases.GetAll;

public class GetAllCustomerQuery : IQuery<List<CustomerDto>>
{
  public string? UsersSales { get; set; }
  public DateTime? DateUsersSales { get; set; }
  public bool Owing { get; set; }
}
