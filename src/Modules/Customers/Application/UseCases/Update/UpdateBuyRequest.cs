using System;

namespace Application.UseCases.Update;

public class UpdateBuyRequest : CustomerActionsResponse
{
  public Guid ProductId { get; set; }
  public string Name { get; set; }
  public double Price { get; set; }
  public int Quantity { get; set; }
}
