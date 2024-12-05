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

            List<Customer> dataToReturn = new List<Customer>();

            if (!string.IsNullOrEmpty(request.UsersSales) && request.Owing)
            {
                result.ForEach(element =>
                {
                    var existsBuys = element.Buys.Exists(filter => filter.UpdatedBy == request.UsersSales);

                    if (existsBuys && element.AmountToPay > 0) dataToReturn.Add(element);
                });
            }
            else if (!string.IsNullOrEmpty(request.UsersSales))
            {
                result.ForEach(element =>
                {
                    var existsBuys = element.Buys.Exists(filter => filter.UpdatedBy == request.UsersSales);

                    if (existsBuys) dataToReturn.Add(element);
                });
            }
            else if (request.Owing)
            {
                result.ForEach(element =>
                {
                    if (element.AmountToPay > 0) dataToReturn.Add(element);
                });
            }
            else
            {
                dataToReturn.AddRange(result);
            }            

            var customer = _mapper.Map<List<CustomerDTO>>(dataToReturn);

            return customer;
        }
    }
}
