using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.GetAllCustomer
{
    public sealed record class GetAllCustomerRequest : IRequest<List<CustomerDTO>>
    {
        public string? UsersSales { get; set; }
        public DateTime? DateUsersSales { get; set; }
        public bool Owing { get; set; }
    }
}
