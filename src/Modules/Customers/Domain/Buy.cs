using BlockDomain.SeedWork;

namespace Domain;

public class Buy : Entity
{
  public string Name { get; set; }
  public double Price { get; set; }
  public int Quantity { get; set; }
  public double Total { get; set; }

  public Buy(double price, int quantity, string name)
  {
    Price = price;
    Quantity = quantity;
    Total = price * quantity;
    Name = name;
  }

  public static Buy NewEntity(Buy buy)
  {
    buy.Id = Guid.NewGuid();
    buy.DateCreated = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time"));

    return buy;
  }
}
