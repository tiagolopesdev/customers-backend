using AutoMapper;
using Product.Domain.Product;

namespace Product.Application.UseCases.UpdateProduct;

public class UpdateProductMapper : Profile
{
  public UpdateProductMapper()
  {
    CreateMap<UpdateProductCommand, ProductAggregateRoot>();
  }
}
