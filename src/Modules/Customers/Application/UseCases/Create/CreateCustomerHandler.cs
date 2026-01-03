using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using BlockApplication.Contracts.Notification;
using Customer.Application.UseCases.UpdateStockProduct;
using Domain.Customers;

namespace Customer.Application.UseCases.Create;

public sealed class CreateCustomerHandler : IHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    //private readonly IMediator _sendNotification;
    private readonly ISendNotification _sendNotification;

    //public CreateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository, IMediator sendNotification)
    public CreateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository, ISendNotification sendNotification)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _sendNotification = sendNotification;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        //var customer = _mapper.Map<CustomerAggregateRoot>(request);

        //customer = CustomerAggregateRoot.NewEntity(customer);

        //customer = EntityInitializer.DefinePaymentEqualsNew(customer);
        //customer = EntityInitializer.DefineBuyEqualsNew(customer);

        //customer.SetAmountToPay();
        //customer.SetAmountPaid();

        //customer = PrecisionValues.PrecisionDecimalValues(customer);

        //customer.AmountPaid = CommonHelpers.CalculatePrecision(customer.AmountPaid);
        //customer.AmountToPay = CommonHelpers.CalculatePrecision(customer.AmountToPay);

        //_customerRepository.Create(customer);


        _ = _sendNotification.Publish(new UpdateStockProductNotification(
            request.Buys.Select(element =>
                new UpdatedStockModel(
                    element.ProductId,
                    element.Quantity
                    )
                ).ToList()), cancellationToken);

        return Guid.NewGuid();
        //return customer.Id;
    }
}
