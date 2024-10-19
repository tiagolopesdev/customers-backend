using AutoMapper;
using Customers.Domain.AggregatesModel.Products;

namespace Customers.Application.UseCases.ProductUseCases.HasStockProduct
{
    public class HasStockProductMapper : Profile
    {
        public HasStockProductMapper()
        {
            CreateMap<Product, HasStockProductResponse>()
                .ForMember(dest => dest.QuantityAvailable, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
