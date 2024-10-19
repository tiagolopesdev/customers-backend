using AutoMapper;
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;

namespace Customers.Application.UseCases.CustomerUseCases.CreateCustomer
{
    public sealed class CreateCustomerMapper : Profile
    {
        public CreateCustomerMapper()
        {
            CreateMap<CreateBuyRequest, BuyEntity>()
                .ForMember(dest => dest.Total, opt => opt.Ignore());

            CreateMap<CreatePaymentRequest, PaymentEntity>();

            CreateMap<CreateCustomerRequest, Customer>()
                .ForMember(dest => dest.Buys, opt => opt.MapFrom(src => src.Buys))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                .ForMember(dest => dest.AmountPaid, opt => opt.Ignore())
                .ForMember(dest => dest.AmountToPay, opt => opt.Ignore());
        }
    }
}
