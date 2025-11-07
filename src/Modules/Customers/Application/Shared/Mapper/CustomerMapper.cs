using Application.Contracts.Mapper;
using Application.Shared.Dtos;
using AutoMapper;
using Domain.Customers;

namespace Application.Shared.Mapper;

public class CustomerMapper : Profile
{
  public CustomerMapper()
  {
    CreateMap<Buy, BuyDto>();

    CreateMap<Payment, PaymentDto>();

    CreateMap<CustomerAggregateRoot, CustomerDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.AmountPaid))
        .ForMember(dest => dest.AmountToPay, opt => opt.MapFrom(src => src.AmountToPay))
        .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
        .ForMember(dest => dest.Buys, opt => opt.MapFrom(src => src.Buys))
        .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
  }
}
