using AutoMapper;
using Customers.Domain.AggregatesModel.Products;

namespace Customers.Application.UseCases.CreateProduct
{
    public sealed class CreateProductMapper : Profile
    {
        public CreateProductMapper()
        {
            CreateMap<CreateProductRequest, Product>();
        }
    }
}
