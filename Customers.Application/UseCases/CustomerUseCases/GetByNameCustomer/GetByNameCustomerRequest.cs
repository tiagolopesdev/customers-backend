using Customers.Application.Shared.DTO;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.GetByNameCustomer
{
    public sealed record class GetByNameCustomerRequest : IRequest<PaginationDto<Customer>>
    {
        public string Name { get; set; }
        public string? UsersSales { get; set; }
        public DateTime? DateUsersSales { get; set; }
        public bool Owing { get; set; }
        public int PageSize { get; set; } = 10; 
        public int PageIndex { get; set; } = 1;
    }
}
