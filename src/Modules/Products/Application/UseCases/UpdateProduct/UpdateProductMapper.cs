using AutoMapper;
using Domain.Product;

namespace Application.UseCases.UpdateProduct;

public class UpdateProductMapper : Profile
{
  public UpdateProductMapper()
  {
    CreateMap<UpdateProductCommand, Product>();
  }
}
