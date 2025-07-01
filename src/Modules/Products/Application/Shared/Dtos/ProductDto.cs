
namespace Application.Shared.Dtos;

public record class ProductDto(Guid Id, string Name, string Description,
  double Value, int Quantity, DateTime DateCreated,
  double BasePrice, int QuantitySold
  )
{

}

