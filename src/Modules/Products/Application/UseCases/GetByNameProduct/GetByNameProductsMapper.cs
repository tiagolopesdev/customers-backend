using Application.Contracts.Mapper;
using Application.Shared.Dtos;
using Domain.Product;

namespace Application.UseCases.GetByNameProduct;

public sealed class GetByNameProductsMapper : MapperProfile
{
  public GetByNameProductsMapper()
  {
    CreateMap<Product, ProductDto>();
  }
}
