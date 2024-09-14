
using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.GetByNameCustomer
{
    public class GetByNameCustomerHandler : IRequestHandler<GetByNameCustomerRequest, List<CustomerDTO>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetByNameCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> Handle(GetByNameCustomerRequest request, CancellationToken cancellationToken)
        {
            List<Customer> result = await _customerRepository.GetByName(request.Name);

            result = CustomerHelper.FilterPropertyListNotDeleted(result);

            var customer = _mapper.Map<List<CustomerDTO>>(result);

            return customer;
        }
    }
}
