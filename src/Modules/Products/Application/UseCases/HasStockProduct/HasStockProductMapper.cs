using AutoMapper;
using Product.Domain.Product;

namespace Product.Application.UseCases.HasStockProduct;

public sealed class HasStockProductMapper : Profile
{
    public HasStockProductMapper()
    {
        CreateMap<ProductAggregateRoot, HasStockDto>()
            .ForMember(dest => dest.QuantityAvailable, opt => opt.MapFrom(src => src.Quantity));
    }
}
