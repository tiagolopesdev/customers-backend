

using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.GetByIdCustomer
{
    public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerRequest, CustomerDTO>
    {
        public readonly ICustomerRepository _customerRepository;
        public readonly IMapper _mapper;

        public GetByIdCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetByIdCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer result = await _customerRepository.GetById(request.Id);

            result = CustomerHelper.FilterPropertyNotDeleted(result);
            result = CustomerHelper.AssignAmountToPay(result);

            var customer = _mapper.Map<CustomerDTO>(result);

            return customer;
        }
    }
}
