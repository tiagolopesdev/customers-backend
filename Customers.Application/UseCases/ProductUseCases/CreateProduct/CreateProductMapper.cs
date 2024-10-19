using AutoMapper;
using Customers.Domain.AggregatesModel.Products;

namespace Customers.Application.UseCases.ProductUseCases.CreateProduct
{
    public sealed class CreateProductMapper : Profile
    {
        public CreateProductMapper()
        {
            CreateMap<CreateProductRequest, Product>();
        }
    }
}
