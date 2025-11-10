using AutoMapper;
using Domain.Product;

namespace Application.UseCases.Create;

public class CreateProductMapper : Profile
{
  public CreateProductMapper()
  {
    CreateMap<CreateProductCommand, Product>();
  }
}
