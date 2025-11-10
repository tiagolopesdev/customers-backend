using MediatR;

namespace Application.UseCases.Create;

public sealed record class CreateProductCommand(string Name, string Description,
  double value, double basePrice, int quantity) : IRequest<Guid>
{ 
  public Guid Id { get; set; }
}