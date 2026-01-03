using BlockApplication.Contracts.CommandQuery;

namespace Product.Application.UseCases.Create;

public sealed record class CreateProductCommand(string Name, string Description,
  double value, double basePrice, int quantity) : ICommand<Guid>
{ 
  public Guid Id { get; set; }
}