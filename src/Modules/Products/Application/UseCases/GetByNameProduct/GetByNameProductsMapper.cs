using Application.Shared.Dtos;
using AutoMapper;
using Domain.Product;

namespace Application.UseCases.GetByNameProduct;

public sealed class GetByNameProductsMapper : Profile
{
    public GetByNameProductsMapper()
    {
        CreateMap<Product, ProductDto>();
    }
}
