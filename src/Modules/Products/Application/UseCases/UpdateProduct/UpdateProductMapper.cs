using Application.Contracts.Mapper;
using Domain.Product;

namespace Application.UseCases.UpdateProduct;

public class UpdateProductMapper : MapperProfile
{
  public UpdateProductMapper()
  {
    CreateMap<UpdateProductCommand, Product>();
  }
}
