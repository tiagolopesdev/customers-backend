using Application.Services;
using Application.Shared.Dtos;
using AutoMapper;
using Domain.Customers;
using MediatR;

namespace Application.UseCases.GetAll;

public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerDto>>
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
