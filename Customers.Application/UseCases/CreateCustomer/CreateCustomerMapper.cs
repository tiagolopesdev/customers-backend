using AutoMapper;
using Customers.Application.UseCases.CreateUser;
using Customers.Domain.AggregatesModel.CustomerAggregate;

namespace Customers.Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerMapper : Profile
    {
        public CreateCustomerMapper()
        {
            CreateMap<CreateCustomerRequest, Customer>();
        }
    }
}
