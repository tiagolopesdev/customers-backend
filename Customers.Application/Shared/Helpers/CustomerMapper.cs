using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;

namespace Customers.Application.Shared.Helpers
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<BuyEntity, BuyDTO>();

            CreateMap<PaymentEntity, PaymentDTO>();

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.AmountToPay, opt => opt.MapFrom(src => src.AmountToPay))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Buys, opt => opt.MapFrom(src => src.Buys))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
        }
    }
}
