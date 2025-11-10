using BlockApplication.Mapper;
using Domain.Product;
using MediatR;


namespace Application.UseCases.Create;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
  private readonly IAutoMapper _mapper;
  private readonly IProductRepository _productRepository;

  public CreateProductHandler(IAutoMapper mapper, IProductRepository productRepository)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var product = _mapper.Map<Product>(request);

    product = Product.NewEntity(product.Name, product.Description, product.Value, product.BasePrice, product.Quantity);

    _productRepository.Create(product);

    return Task.FromResult(product.Id);
  }
}