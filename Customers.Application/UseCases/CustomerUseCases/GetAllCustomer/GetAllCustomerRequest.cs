﻿using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.CustomerUseCases.GetAllCustomer
{
    public sealed record class GetAllCustomerRequest : IRequest<List<CustomerDTO>>
    {
    }
}
