
using AutoMapper;
using Customers.Application.UseCases.CreateUser;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);

            customer.SetAmountPaid();
            customer.SetAmountToPay();

            _customerRepository.Create(customer);

            return customer.Id;
        }
    }
}
