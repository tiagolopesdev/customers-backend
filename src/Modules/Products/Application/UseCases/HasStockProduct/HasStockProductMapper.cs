using Application.Contracts.Mapper;

namespace Application.UseCases.HasStockProduct;

public sealed class HasStockProductMapper : MapperProfile
{
  public HasStockProductMapper()
  {
    CreateMap<Product, HasStockDto>()
        .ForMember(dest => dest.QuantityAvailable, opt => opt.MapFrom(src => src.Quantity));
  }
}
