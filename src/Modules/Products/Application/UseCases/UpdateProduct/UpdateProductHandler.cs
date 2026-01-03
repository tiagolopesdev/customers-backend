using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using BlockApplication.Helpers;
using BlockInfrastructure.EventBus;
using Product.Domain.Product;

namespace Product.Application.UseCases.UpdateProduct;

public class UpdateProductHandler : IHandler<UpdateProductCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IEventBus _eventBus;
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, IEventBus eventBus)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _eventBus = eventBus;
        ExecuteEventBus();
        Console.WriteLine(">>>> dkslkdsldlsklds");
    }
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _productRepository.GetById(request.Id);

        if (productExists == null) throw new Exception("Produto não encontrado para ser atualizado");

        var productToSave = _mapper.Map<ProductAggregateRoot>(request);

        if (request.IsEnable)
        {
            productToSave.DateDeleted = CommonHelpers.DateTimeForBrazil();
        }

        ProductAggregateRoot.UpdatedProduct(productToSave, productExists.DateCreated, request.UpdatedBy);

        await _productRepository.Update(productToSave);

        return productToSave.Id;
    }

    void ExecuteEventBus()
    {
        _eventBus.Subscribe();
        _eventBus.StartConsuming("update-stock");
        Console.WriteLine(">>>> Finish method");
    }
}
