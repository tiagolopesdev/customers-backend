using AutoMapper;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.ValidatePayment
{
    public class ValidatePaymentHandler : IRequestHandler<ValidatePaymentRequest, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ValidatePaymentHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ValidatePaymentRequest request, CancellationToken cancellationToken)
        {
            Customer result = await _customerRepository.GetById(request.CustomerId);

            return request.Value > (result.AmountToPay - result.AmountPaid) ? false : true;
        }
    }// result.AmountToPay - result.AmountPaid
}
