
using AutoMapper;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.CreateCustomer
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

            customer = Customer.NewEntity(customer);

            customer = DefinePaymentEqualsNew(customer);
            customer = DefineBuyEqualsNew(customer);

            customer.SetAmountToPay();
            customer.SetAmountPaid();

            customer = CustomerHelper.PrecisionDecimalValues(customer);

            customer.AmountPaid = Utilities.CalculatePrecision(customer.AmountPaid);
            customer.AmountToPay = Utilities.CalculatePrecision(customer.AmountToPay);

            _customerRepository.Create(customer);

            return customer.Id;
        }

        public static Customer DefineBuyEqualsNew(Customer customer)
        {
            var paymentList = new List<BuyEntity>();

            if (customer.Buys != null && customer.Buys.Count > 0)
            {
                var paymentForAssign = new BuyEntity(0.0, 0, "");

                foreach (var item in customer.Buys)
                {
                    paymentForAssign = BuyEntity.NewEntity(item);
                    paymentForAssign = item;
                }
                paymentList.Add(paymentForAssign);
            }

            return customer;
        }

        public static Customer DefinePaymentEqualsNew(Customer customer)
        {
            var paymentList = new List<PaymentEntity>();

            if (customer.Payments != null && customer.Payments.Count > 0)
            {
                var paymentForAssign = new PaymentEntity(0.0);

                foreach (var item in customer.Payments)
                {
                    paymentForAssign = PaymentEntity.NewEntity(item);
                    paymentForAssign = item;
                }
                paymentList.Add(paymentForAssign);
            }

            return customer;
        }
    }
}
