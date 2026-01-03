using System;
using Domain.Customers;

namespace Customer.Application.Shared.Dtos;

public class PaymentDto
{
  public Guid Id { get; set; }
  public double Value { get; set; }
  public DateTime? DateCreated { get; set; }
  public string UpdatedBy { get; set; }
  public PaymentMethod PaymentMethod { get; set; }
}
