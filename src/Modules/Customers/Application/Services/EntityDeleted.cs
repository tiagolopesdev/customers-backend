using System;
using BlockApplication.Helpers;
using Domain.Customers;

namespace Customer.Application.Services;

public static class EntityDeleted
{
  public static List<CustomerAggregateRoot> FilterPropertyListNotDeleted(List<CustomerAggregateRoot> customer)
  {
    List<CustomerAggregateRoot> customerList = new();

    foreach (var customerItem in customer)
    {
      customerList.Add(FilterPropertyNotDeleted(customerItem));
    }
    return customerList;
  }

  public static CustomerAggregateRoot FilterPropertyNotDeleted(CustomerAggregateRoot customer)
  {
    if (customer.Payments != null && customer.Payments.Count > 0)
    {
      customer.Payments = CommonHelpers.FilterNotDeleteEntity(customer.Payments);
    }
    if (customer.Buys != null && customer.Buys.Count > 0)
    {
      customer.Buys = CommonHelpers.FilterNotDeleteEntity(customer.Buys);
    }
    return customer;
  }
}
