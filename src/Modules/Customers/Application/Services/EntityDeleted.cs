using System;
using BlockApplication.Helpers;
using Domain;

namespace Application.Services;

public static class EntityDeleted
{
  public static List<Customer> FilterPropertyListNotDeleted(List<Customer> customer)
  {
    List<Customer> customerList = new();

    foreach (var customerItem in customer)
    {
      customerList.Add(FilterPropertyNotDeleted(customerItem));
    }
    return customerList;
  }

  public static Customer FilterPropertyNotDeleted(Customer customer)
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
