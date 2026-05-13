using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using Customers.Domain.SeedWork;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.GetByNameProduct
{
    public class GetByNameProductHandler : IRequestHandler<GetByNameProductRequest, PaginationDto<Product>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetByNameProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<PaginationDto<Product>> Handle(GetByNameProductRequest request, CancellationToken cancellationToken)
        {
            Pagination<Product> result = string.IsNullOrEmpty(request.Name) ?
                await _productRepository.GetAll(request.PageIndex, request.PageSize) :
                await _productRepository.GetByName(request.Name, request.PageIndex, request.PageSize);

            var productsToReturn = _mapper.Map<PaginationDto<Product>>(result);

            return productsToReturn;
        }
    }
}
