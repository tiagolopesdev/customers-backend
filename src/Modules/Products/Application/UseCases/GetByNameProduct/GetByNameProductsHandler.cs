using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using BlockApplication.Helpers;
using Product.Application.Shared.Dtos;
using Product.Domain.Product;

namespace Product.Application.UseCases.GetByNameProduct;

public class GetByNameProductsHandler : IHandler<GetByNameProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetByNameProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<List<ProductDto>> Handle(GetByNameProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductAggregateRoot> products = string.IsNullOrEmpty(request.Name) ?
                    await _productRepository.GetAll() :
                    await _productRepository.GetByName(request.Name);

        products = CommonHelpers.FilterNotDeleteEntity(products);

        var productsToReturn = _mapper.Map<List<ProductDto>>(products);

        return productsToReturn;
    }
}
