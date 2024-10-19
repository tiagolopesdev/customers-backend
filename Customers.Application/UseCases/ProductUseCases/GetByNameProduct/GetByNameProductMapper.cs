using AutoMapper;
using Customers.Domain.AggregatesModel.Products;

namespace Customers.Application.UseCases.ProductUseCases.GetByNameProduct
{
    public sealed class GetByNameProductMapper : Profile
    {
        public GetByNameProductMapper()
        {
            CreateMap<Product, GetByNameProductResponse>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            //.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            //.ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));
        }
    }
}
