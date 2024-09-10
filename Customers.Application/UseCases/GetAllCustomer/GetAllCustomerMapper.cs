using AutoMapper;
using Customers.Application.Shared.DTO;
using Customers.Application.UseCases.CreateUser;
using Customers.Domain.AggregatesModel.Buy;
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;

namespace Customers.Application.UseCases.GetAllCustomer
{
    public class GetAllCustomerMapper : Profile
    {
        public GetAllCustomerMapper()
        {
            CreateMap<BuyEntity, BuyDTO>();

            CreateMap<PaymentEntity, PaymentDTO>();

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Supondo que a classe `Entity` tenha o campo `Id`
                .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.GetType().GetProperty("AmountPaid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(src)))
                .ForMember(dest => dest.AmountToPay, opt => opt.MapFrom(src => src.GetType().GetProperty("AmountToPay", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(src)))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Buys, opt => opt.MapFrom(src => src.Buys))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
        }
    }
}
