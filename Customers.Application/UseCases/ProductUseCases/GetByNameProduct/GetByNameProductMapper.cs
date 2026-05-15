using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.SeedWork;

namespace Customers.Application.UseCases.ProductUseCases.GetByNameProduct
{
    public sealed class GetByNameProductMapper : Profile
    {
        public GetByNameProductMapper()
        {
            CreateMap<Pagination<Product>, PaginationDto<Product>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.HasMore, opt => opt.MapFrom(src => src.HasMore))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.TotalItens, opt => opt.MapFrom(src => src.TotalItens))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages))
                ;
            //CreateMap<Product, GetByNameProductResponse>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            //.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            //.ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));
        }
    }
}
