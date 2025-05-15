
using Application.Contracts;
using Application.Services;
using BlockApplication.Helpers;
using BlockApplication.Mapper;
using Domain.Customers;

namespace Application.UseCases.Create;

public sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, Guid>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IAutoMapper _mapper;

  public CreateCustomerHandler(IAutoMapper mapper, ICustomerRepository customerRepository)
  {
    _customerRepository = customerRepository;
    _mapper = mapper;
  }

  public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
  {
    var customer = _mapper.Map<Customer>(request);

    customer = Customer.NewEntity(customer);

    customer = EntityNew.DefinePaymentEqualsNew(customer);
    customer = EntityNew.DefineBuyEqualsNew(customer);

    customer.SetAmountToPay();
    customer.SetAmountPaid();

    customer = PrecisionValues.PrecisionDecimalValues(customer);

    customer.AmountPaid = CommonHelpers.CalculatePrecision(customer.AmountPaid);
    customer.AmountToPay = CommonHelpers.CalculatePrecision(customer.AmountToPay);

    _customerRepository.Create(customer);

    // _mediator.Publish(
    //     new UpdateStockProductNotification(
    //             request.Buys.Select(element => new UpdateStockProductModel(
    //                 element.ProductId,
    //                 element.Quantity
    //                 )
    //             )
    //             .ToList()
    //         )
    //     );

    return customer.Id;
  }
}
