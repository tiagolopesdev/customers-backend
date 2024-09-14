using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.GetAllCustomer
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerRequest, List<CustomerDTO>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> Handle(GetAllCustomerRequest request, CancellationToken cancellationToken)
        {
            List<Customer> result = await _customerRepository.GetAll();

            foreach (var customerItem in result)
            {
                if (customerItem.Payments != null && customerItem.Payments.Count > 0)
                {
                    customerItem.Payments = customerItem.Payments.Where(filter => filter.DateDeleted == null).ToList();
                }
                if (customerItem.Buys != null && customerItem.Buys.Count > 0)
                {
                    customerItem.Buys = customerItem.Buys.Where(filter => filter.DateDeleted == null).ToList();
                }
            }

            var customer = _mapper.Map<List<CustomerDTO>>(result);

            return customer;
        }
    }
}
