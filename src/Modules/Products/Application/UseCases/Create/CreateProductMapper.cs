using AutoMapper;
using Product.Domain.Product;

namespace Product.Application.UseCases.Create;

public class CreateProductMapper : Profile
{
  public CreateProductMapper()
  {
    CreateMap<CreateProductCommand, ProductAggregateRoot>();
  }
}
