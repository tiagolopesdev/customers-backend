using System;
using Application.Contracts.Mapper;
using Domain.Product;

namespace Application.UseCases.Create;

public class CreateProductMapper : MapperProfile
{
  public CreateProductMapper()
  {
    CreateMap<CreateProductCommand, Product>();
  }
}
