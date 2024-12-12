using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.GetAllCustomer
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

            result = CustomerHelper.FilterPropertyListNotDeleted(result);

            result = CustomerHelper.AssignAmountToPayList(result);

            List<Customer> dataToReturn = [.. CustomerHelper.ApplyingFilters(result, request.Owing, request.UsersSales, request.DateUsersSales)];
        
            var customer = _mapper.Map<List<CustomerDTO>>(dataToReturn);

            return customer;
        }
    }
}
