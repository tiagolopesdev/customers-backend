using Application.Services;
using Application.Shared.Dtos;
using AutoMapper;
using Domain.Customers;
using MediatR;

namespace Application.UseCases.GetById;

public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQuery, CustomerDto>
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
