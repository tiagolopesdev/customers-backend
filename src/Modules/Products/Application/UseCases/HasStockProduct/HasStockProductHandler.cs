using Application.Contracts.Query;
using BlockApplication.Mapper;
using Domain.Product;

namespace Application.UseCases.HasStockProduct;

public class HasStockProductHandler : IQueryHandler<HasStockProductQuery, HasStockDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IAutoMapper _mapper;

    public HasStockProductHandler(IProductRepository productRepository, IAutoMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<HasStockDto> Handle(HasStockProductQuery request, CancellationToken cancellationToken)
    {
        Product result = await _productRepository.GetById(request.Id);

        HasStockDto response = _mapper.Map<HasStockDto>(result);

        return response;
    }
}
