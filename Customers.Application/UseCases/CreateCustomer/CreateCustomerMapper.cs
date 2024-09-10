using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Application.UseCases.CreateUser;
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;

namespace Customers.Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerMapper : Profile
    {
        public CreateCustomerMapper()
        {
            CreateMap<CreateBuyRequest, BuyEntity>()
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());
            
            CreateMap<CreatePaymentRequest, PaymentEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());

            CreateMap<CreateCustomerRequest, Customer>()
                .ForMember(dest => dest.Buys, opt => opt.MapFrom(src => src.Buys))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.AmountPaid, opt => opt.Ignore())
                .ForMember(dest => dest.AmountToPay, opt => opt.Ignore());
        }
    }
}
