using AutoMapper;
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
            customerToSave.Buys = AssignDateActions(
                customerToSave.Buys,
                request.Buys,
                customerFounded.Buys
                );

            customerToSave.Id = customerFounded.Id;
            customerToSave.DateCreated = customerFounded.DateCreated;

            customerToSave.SetAmountPaid();
            customerToSave.SetAmountToPay();

            await _customerRepository.UpdateCustomer(customerToSave);

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
                T? entity = LogicFindEntity(customer, item.Id);

                entity.DateCreated = LogicFindEntity(customerFounded, item.Id).DateCreated;

                if (item.IsEnable is true && customer != null && customer.Count > 0)
                {
                    LogicFindEntity(customer, item.Id).DateDeleted = DateTime.Now;
                }
                LogicFindEntity(customer, item.Id).DateUpdated = DateTime.Now;
            }
            return customer;
        }
    }
}
