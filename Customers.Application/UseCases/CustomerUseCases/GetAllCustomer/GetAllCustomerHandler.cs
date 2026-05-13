using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using Customers.Domain.SeedWork;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.GetAllCustomer
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerRequest, PaginationDto<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PaginationDto<Customer>> Handle(GetAllCustomerRequest request, CancellationToken cancellationToken)
        {
            Pagination<Customer> result = await _customerRepository.GetAll(request.PageIndex, request.PageSize, request.Owing);

            result.Data = CustomerHelper.FilterPropertyListNotDeleted(result.Data);

            result.Data = CustomerHelper.AssignAmountToPayList(result.Data);

            result.Data = [.. CustomerHelper.ApplyingFilters(result.Data, request.Owing, request.UsersSales, request.DateUsersSales)];
        
            var paginatedData = _mapper.Map<PaginationDto<Customer>>(result);

            return paginatedData;
        }
    }
}
