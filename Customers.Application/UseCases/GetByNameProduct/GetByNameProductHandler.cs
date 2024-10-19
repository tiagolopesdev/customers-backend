using AutoMapper;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.GetByNameProduct
{
    public class GetByNameProductHandler : IRequestHandler<GetByNameProductRequest, List<GetByNameProductResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetByNameProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<GetByNameProductResponse>> Handle(GetByNameProductRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository.GetByName(request.Name);

            products = Utilities.FilterNotDeleteEntity(products);

            var productsToReturn = _mapper.Map<List<GetByNameProductResponse>>(products);

            return productsToReturn;
        }
    }
}
