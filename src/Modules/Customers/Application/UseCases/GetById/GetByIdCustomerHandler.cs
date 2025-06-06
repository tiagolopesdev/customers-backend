using Application.Contracts.Query;
using Application.Services;
using Application.Shared.Dtos;
using BlockApplication.Mapper;
using Domain.Customers;

namespace Application.UseCases.GetById;

public class GetByIdCustomerHandler : IQueryHandler<GetByIdCustomerQuery, CustomerDto>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IAutoMapper _mapper; 

  public GetByIdCustomerHandler(ICustomerRepository customerRepository, IAutoMapper mapper)
  {
    _customerRepository = customerRepository;
    _mapper = mapper;
  }

  public async Task<CustomerDto> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
  {
    Customer result = await _customerRepository.GetById(request.Id);

    result = EntityDeleted.FilterPropertyNotDeleted(result);
    result = Customer.AssignAmountToPay(result);

    var customer = _mapper.Map<CustomerDto>(result);

    return customer;
  }
}
