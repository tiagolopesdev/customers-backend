using Application.Contracts.Query;
using Application.Services;
using Application.Shared.Dtos;
using BlockApplication.Mapper;
using Domain.Customers;

namespace Application.UseCases.GetByName;

public class GetByNameHandler : IQueryHandler<GetByNameQuery, List<CustomerDto>>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IAutoMapper _mapper;

  public GetByNameHandler(ICustomerRepository customerRepository, IAutoMapper mapper)
  {
    _customerRepository = customerRepository;
    _mapper = mapper;
  }
  public async Task<List<CustomerDto>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
  {
    List<Customer> result = await _customerRepository.GetByName(request.Name);

    result = EntityDeleted.FilterPropertyListNotDeleted(result);

    result = CallMethodBulk.AssignAmountToPayList(result);

    List<Customer> dataToReturn = [.. Filters.ApplyingFilters(result, request.Owing, request.UsersSales, request.DateUsersSales)];

    var customer = _mapper.Map<List<CustomerDto>>(dataToReturn);

    return customer;
  }
}
