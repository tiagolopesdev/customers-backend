using BlockDomain.SeedWork;

namespace Domain.Product;

public class Product : Entity, IAggregateRoot
{
  public string Name { get; set; }
  public string Description { get; set; }
  public double Value { get; set; }
  public double BasePrice { get; set; }
  public int Quantity { get; set; }
  public int QuantitySold { get; set; }

  public static Product NewEntity(string name, string description, double value, double basePrice, int quantity)
  {
    return new()
    {
      Id = Guid.NewGuid(),
      DateCreated = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time")),
      Name = name,
      Description = description,
      Value = value,
      BasePrice = basePrice,
      Quantity = quantity,
      QuantitySold = 0
    };
  }
}
