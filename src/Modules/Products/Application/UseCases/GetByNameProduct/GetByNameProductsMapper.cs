using AutoMapper;
using Product.Application.Shared.Dtos;
using Product.Domain.Product;

namespace Product.Application.UseCases.GetByNameProduct;

public sealed class GetByNameProductsMapper : Profile
{
    public GetByNameProductsMapper()
    {
        CreateMap<ProductAggregateRoot, ProductDto>();
    }
}
