using Application.Services;
using Application.Shared.Dtos;
using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using Domain.Customers;

namespace Application.UseCases.GetByName;

public class GetByNameHandler : IHandler<GetByNameQuery, List<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetByNameHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    public async Task<List<CustomerDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        List<CustomerAggregateRoot> result = await _customerRepository.GetByName(request.Name);

        result = EntityDeleted.FilterPropertyListNotDeleted(result);

        result = CallMethodBulk.AssignAmountToPayList(result);

        List<CustomerAggregateRoot> dataToReturn = [.. Filters.ApplyingFilters(result, request.Owing, request.UsersSales, request.DateUsersSales)];

        var customer = _mapper.Map<List<CustomerDto>>(dataToReturn);

        return customer;
    }
}
