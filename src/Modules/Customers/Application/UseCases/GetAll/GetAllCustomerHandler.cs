using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using Customer.Application.Services;
using Customer.Application.Shared.Dtos;
using Domain.Customers;

namespace Customer.Application.UseCases.GetAll;

public class GetAllCustomerHandler : IHandler<GetAllCustomerQuery, List<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<CustomerAggregateRoot> result = await _customerRepository.GetAll();

        result = CallMethodBulk.AssignAmountToPayList(result);

        result = EntityDeleted.FilterPropertyListNotDeleted(result);

        List<CustomerAggregateRoot> dataToReturn = [.. Filters.ApplyingFilters(result, request.Owing, request.UsersSales, request.DateUsersSales)];

        var customer = _mapper.Map<List<CustomerDto>>(dataToReturn);

        return customer;
    }
}
