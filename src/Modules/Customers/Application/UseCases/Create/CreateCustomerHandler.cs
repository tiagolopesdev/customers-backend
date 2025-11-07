
using Application.Services;
using AutoMapper;
using BlockApplication.Helpers;
using Domain.Customers;
using MediatR;

namespace Application.UseCases.Create;

public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<CustomerAggregateRoot>(request);

        customer = CustomerAggregateRoot.NewEntity(customer);

        customer = EntityInitializer.DefinePaymentEqualsNew(customer);
        customer = EntityInitializer.DefineBuyEqualsNew(customer);

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
