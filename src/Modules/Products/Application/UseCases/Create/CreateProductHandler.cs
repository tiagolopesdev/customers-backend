using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using Product.Domain.Product;


namespace Product.Application.UseCases.Create;

public class CreateProductHandler : IHandler<CreateProductCommand, Guid>
{
  private readonly IMapper _mapper;
  private readonly IProductRepository _productRepository;

  public CreateProductHandler(IMapper mapper, IProductRepository productRepository)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var product = _mapper.Map<ProductAggregateRoot>(request);

    product = ProductAggregateRoot.NewEntity(product.Name, product.Description, product.Value, product.BasePrice, product.Quantity);

    _productRepository.Create(product);

    return Task.FromResult(product.Id);
  }
}