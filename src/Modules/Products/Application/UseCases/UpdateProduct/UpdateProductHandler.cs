using Application.Contracts;
using BlockApplication.Helpers;
using BlockApplication.Mapper;
using Domain.Product;

namespace Application.UseCases.UpdateProduct;

public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, Guid>
{
  private readonly IAutoMapper _mapper;
  private readonly IProductRepository _productRepository;
  public UpdateProductHandler(IProductRepository productRepository, IAutoMapper mapper)
  {
    _mapper = mapper;
    _productRepository = productRepository;
  }
  public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    var productExists = await _productRepository.GetById(request.Id);

    if (productExists == null) throw new Exception("Produto não encontrado para ser atualizado");

    var productToSave = _mapper.Map<Product>(request);

    if (request.IsEnable)
    {
      productToSave.DateDeleted = CommonHelpers.DateTimeForBrazil();
    }

    productToSave.DateCreated = productExists.DateCreated;
    productToSave.SetDateUpdated();
    productToSave.UpdatedBy = request.UpdatedBy;

    await _productRepository.Update(productToSave);

    return productToSave.Id;
  }
}
