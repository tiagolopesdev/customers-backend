using Application.Shared.Dtos;
using BlockApplication.Helpers;
using BlockApplication.Mapper;
using Domain.Product;
using MediatR;

namespace Application.UseCases.GetByNameProduct;

public class GetByNameProductsHandler : IRequestHandler<GetByNameProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IAutoMapper _mapper;

    public GetByNameProductsHandler(IProductRepository productRepository, IAutoMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<List<ProductDto>> Handle(GetByNameProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = string.IsNullOrEmpty(request.Name) ?
                    await _productRepository.GetAll() :
                    await _productRepository.GetByName(request.Name);

        products = CommonHelpers.FilterNotDeleteEntity(products);

        var productsToReturn = _mapper.Map<List<ProductDto>>(products);

        return productsToReturn;
    }
}
