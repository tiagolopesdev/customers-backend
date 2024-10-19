
using AutoMapper;
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, Guid>
    {
        public readonly IMapper _mapper;
        public readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {

            var product = _mapper.Map<Product>(request);

            product = Product.NewEntity(product);

            _productRepository.Create(product);

            return product.Id;
        }
    }
}
