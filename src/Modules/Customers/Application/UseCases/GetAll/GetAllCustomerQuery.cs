
using Application.Shared.Dtos;
using MediatR;

namespace Application.UseCases.GetAll;

//public class GetAllCustomerQuery : IQuery<List<CustomerDto>>
public class GetAllCustomerQuery : IRequest<List<CustomerDto>>
{
    public string? UsersSales { get; set; }
    public DateTime? DateUsersSales { get; set; }
    public bool Owing { get; set; }
}
