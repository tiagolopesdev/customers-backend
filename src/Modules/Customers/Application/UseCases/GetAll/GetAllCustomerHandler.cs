using Application.Contracts.Query;
using Application.Services;
using Application.Shared.Dtos;
using BlockApplication.Mapper;
using Domain.Customers;

namespace Application.UseCases.GetAll;

public class GetAllCustomerHandler : IQueryHandler<GetAllCustomerQuery, List<CustomerDto>>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IAutoMapper _mapper;

  public GetAllCustomerHandler(ICustomerRepository customerRepository, IAutoMapper mapper)
  {
    _mapper = mapper;
    _customerRepository = customerRepository;
  }

  public async Task<List<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
  {
    List<Customer> result = await _customerRepository.GetAll();

    result = CallMethodBulk.AssignAmountToPayList(result);

    result = EntityDeleted.FilterPropertyListNotDeleted(result);

    List<Customer> dataToReturn = [.. Filters.ApplyingFilters(result, request.Owing, request.UsersSales, request.DateUsersSales)];

    var customer = _mapper.Map<List<CustomerDto>>(dataToReturn);

    return customer;
  }
}
