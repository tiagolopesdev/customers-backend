﻿using MediatR;

namespace Customers.Application.UseCases.UpdateCustomer
{
    public abstract class CustomerActionsResponse
    {
        public Guid Id { get; set; }
        public bool IsEnable { get; set; }
    }
    public sealed record class UpdateCustomerRequest() : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UpdatePaymentRequest> Payments { get; set; }
        public List<UpdateBuyRequest> Buys { get; set; }
        public bool IsEnable { get; set; }
    }

    public sealed class UpdateBuyRequest : CustomerActionsResponse
    {
        //public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }        
        //public bool IsEnable { get; set; }
    }

    public sealed class UpdatePaymentRequest : CustomerActionsResponse
    {
        //public Guid Id { get; set; }
        public double Value { get; set; }
        //public bool IsEnable { get; set; }
    }
}
