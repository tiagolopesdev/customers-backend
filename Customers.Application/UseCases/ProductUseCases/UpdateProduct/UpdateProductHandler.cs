
using AutoMapper;
using Customers.Application.Shared.Helpers;
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Guid>
    {
        public readonly IMapper _mapper;
        public readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var productExists = await _productRepository.GetById(request.Id);

            if (productExists == null) throw new Exception("Produto não encontrado para ser atualizado");

            var productToSave = _mapper.Map<Product>(request);

            if (request.IsEnable)
            {
                productToSave.DateDeleted = Utilities.DateTimeForBrazil();
            }

            productToSave.DateCreated = productExists.DateCreated;
            productToSave.SetDateUpdated();
            productToSave.UpdatedBy = request.UpdatedBy;
            
            await _productRepository.Update(productToSave);

            return productToSave.Id;
        }
    }
}
