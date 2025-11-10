using AutoMapper;
using BlockApplication.Helpers;
using Domain.Product;
using MediatR;

namespace Application.UseCases.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
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

        Product.UpdatedProduct(productToSave, productExists.DateCreated, request.UpdatedBy);

        await _productRepository.Update(productToSave);

        return productToSave.Id;
    }
}
