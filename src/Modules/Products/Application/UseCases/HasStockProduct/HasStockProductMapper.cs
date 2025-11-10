using AutoMapper;
using Domain.Product;

namespace Application.UseCases.HasStockProduct;

public sealed class HasStockProductMapper : Profile
{
    public HasStockProductMapper()
    {
        CreateMap<Product, HasStockDto>()
            .ForMember(dest => dest.QuantityAvailable, opt => opt.MapFrom(src => src.Quantity));
    }
}
