namespace Customer.Application.UseCases.Create;

public class CreateBuyCommand
{
  public Guid ProductId { get; set; }
  public string Name { get; set; }
  public double Price { get; set; }
  public int Quantity { get; set; }
  public string UpdatedBy { get; set; }
}
