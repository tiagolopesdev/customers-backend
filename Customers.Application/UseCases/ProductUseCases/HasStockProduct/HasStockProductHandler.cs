
using AutoMapper;
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.HasStockProduct
{
    public class HasStockProductHandler : IRequestHandler<HasStockProductRequest, HasStockProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public HasStockProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<HasStockProductResponse> Handle(HasStockProductRequest request, CancellationToken cancellationToken)
        {
            Product result = await _productRepository.GetById(request.Id);

            HasStockProductResponse response = _mapper.Map<HasStockProductResponse>(result);

            return response;
        }
    }
}
