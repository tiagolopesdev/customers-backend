using Application.Contracts;
using Application.Services;
using BlockApplication.Helpers;
using BlockApplication.Mapper;
using BlockDomain.SeedWork;
using Domain.Customers;
using static Application.UseCases.Update.UpdateCustomerComand;

namespace Application.UseCases.Update;

public class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand, Guid>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IAutoMapper _mapper;

  public UpdateCustomerHandler(ICustomerRepository customerRepository, IAutoMapper mapper)
  {
    _customerRepository = customerRepository;
    _mapper = mapper;
  }

  public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
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
        item.DateCreated = CommonHelpers.DateTimeForBrazil();
      }
      else
      {
        item.DateUpdated = CommonHelpers.DateTimeForBrazil();
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
        item.DateCreated = CommonHelpers.DateTimeForBrazil();
      }
      else
      {
        item.DateUpdated = CommonHelpers.DateTimeForBrazil();
      }
    });

    customerToSave.Id = customerFounded.Id;
    customerToSave.DateCreated = customerFounded.DateCreated;

    foreach (var item in customerFounded.Buys)
    {
      var result = customerToSave.Buys.Find(searchElement => searchElement.Id == item.Id);

      if (result == null)
      {
        customerToSave.Buys.Add(item);
      }
    }

    foreach (var item in customerFounded.Payments)
    {
      var result = customerToSave.Payments.Find(searchElement => searchElement.Id == item.Id);

      if (result == null)
      {
        customerToSave.Payments.Add(item);
      }
    }

    customerToSave.SetAmountPaid();
    customerToSave.SetAmountToPay();

    customerToSave.DateUpdated = CommonHelpers.DateTimeForBrazil();

    customerToSave = PrecisionValues.PrecisionDecimalValues(customerToSave);

    customerToSave.AmountPaid = CommonHelpers.CalculatePrecision(customerToSave.AmountPaid);
    customerToSave.AmountToPay = CommonHelpers.CalculatePrecision(customerToSave.AmountToPay);

    if (customerToSave.AmountPaid > customerToSave.AmountToPay)
    {
      throw new Exception("Valor maior que saldo a pagar");
    }

    await _customerRepository.UpdateCustomer(customerToSave);

    var products = request.Buys.Where(filter => filter.ProductId != Guid.Empty);

    _mediator.Publish(
        new UpdateStockProductNotification(
            products.Select(element => new UpdateStockProductModel(
                    element.ProductId,
                    element.Quantity
                    )
                )
                .ToList()
            )
        );

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
        LogicFindEntity(customer, (Guid)item.Id).DateDeleted = CommonHelpers.DateTimeForBrazil();
      }
      LogicFindEntity(customer, (Guid)item.Id).DateUpdated = CommonHelpers.DateTimeForBrazil();
    }
    return customer;
  }
}
