using AutoMapper;
using BlockApplication.Contracts.CommandQuery;
using Product.Domain.Product;

namespace Product.Application.UseCases.HasStockProduct;

public class HasStockProductHandler : IHandler<HasStockProductQuery, HasStockDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public HasStockProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<HasStockDto> Handle(HasStockProductQuery request, CancellationToken cancellationToken)
    {
        ProductAggregateRoot result = await _productRepository.GetById(request.Id);

        HasStockDto response = _mapper.Map<HasStockDto>(result);

        return response;
    }
}
