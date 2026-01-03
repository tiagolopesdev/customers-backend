using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using Customer.Application.Services;
using Customer.Application.Shared.Dtos;
using Domain.Customers;

namespace Customer.Application.UseCases.GetById;

public class GetByIdCustomerHandler : IHandler<GetByIdCustomerQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetByIdCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        CustomerAggregateRoot result = await _customerRepository.GetById(request.Id);

        result = EntityDeleted.FilterPropertyNotDeleted(result);
        result = CustomerAggregateRoot.AssignAmountToPay(result);

        var customer = _mapper.Map<CustomerDto>(result);

        return customer;
    }
}
