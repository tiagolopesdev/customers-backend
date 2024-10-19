using AutoMapper;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using Customers.Domain.SeedWork;
using MediatR;

namespace Customers.Application.UseCases.UpdateCustomer
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customerFounded = await _customerRepository.GetById(request.Id);

            if (customerFounded == null) throw new Exception("Cliente não encontrado para ser atualizado");

            var customerToSave = _mapper.Map<Customer>(request);

            customerToSave.Payments = AssignDateActions(
                customerToSave.Payments,
                request.Payments,
                customerFounded.Payments
                );

            customerToSave.Payments.ForEach(item =>
            {
                if (item.Id == null || item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                    item.DateCreated = DateTime.Now;
                } else
                {
                    item.DateUpdated = DateTime.Now;
                }
            });

            customerToSave.Buys = AssignDateActions(
                customerToSave.Buys,
                request.Buys,
                customerFounded.Buys
                );

            customerToSave.Buys.ForEach(item =>
            {
                if (item.Id == null || item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                    item.DateCreated = DateTime.Now;
                }
                else
                {
                    item.DateUpdated = DateTime.Now;
                }
            });

            customerToSave.Id = customerFounded.Id;
            customerToSave.DateCreated = customerFounded.DateCreated;

            customerToSave.SetAmountPaid();
            customerToSave.SetAmountToPay();

            customerToSave.DateUpdated = DateTime.Now;

            customerToSave = CustomerHelper.PrecisionDecimalValues(customerToSave);

            customerToSave.AmountPaid = Utilities.CalculatePrecision(customerToSave.AmountPaid);
            customerToSave.AmountToPay = Utilities.CalculatePrecision(customerToSave.AmountToPay);

            await _customerRepository.Update(customerToSave);

            return customerFounded.Id;
        }

        public static T? LogicFindEntity<T>(List<T> entity, Guid key) where T : Entity
        {
            return entity.Find(element => element.Id == key);
        }

        public static List<T> AssignDateActions<T, R>(List<T> customer, List<R> request, List<T> customerFounded)
            where T : Entity
            where R : CustomerActionsResponse
        {
            foreach (var item in request)
            {
                if (item.Id == null || item.Id == Guid.Empty) continue;

                T? entity = LogicFindEntity(customer, (Guid)item.Id);

                entity.DateCreated = LogicFindEntity(customerFounded, (Guid)item.Id).DateCreated;

                if (item.IsEnable is true && customer != null && customer.Count > 0)
                {
                    LogicFindEntity(customer, (Guid)item.Id).DateDeleted = DateTime.Now;
                }
                LogicFindEntity(customer, (Guid)item.Id).DateUpdated = DateTime.Now;
            }
            return customer;
        }
    }
}
