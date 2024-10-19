
using AutoMapper;
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;

namespace Customers.Application.UseCases.CustomerUseCases.UpdateCustomer
{
    public class UpdateCustomerMapper : Profile
    {
        public UpdateCustomerMapper()
        {
            CreateMap<UpdateBuyRequest, BuyEntity>()
                .ForMember(dest => dest.Total, opt => opt.Ignore());

            CreateMap<UpdatePaymentRequest, PaymentEntity>();

            CreateMap<UpdateCustomerRequest, Customer>()
                .ForMember(dest => dest.Buys, opt => opt.MapFrom(src => src.Buys))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.AmountPaid, opt => opt.Ignore())
                .ForMember(dest => dest.AmountToPay, opt => opt.Ignore());
        }
    }
}
