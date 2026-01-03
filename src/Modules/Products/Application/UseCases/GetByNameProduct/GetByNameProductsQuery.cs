using BlockApplication.Contracts.CommandQuery;
using Product.Application.Shared.Dtos;

namespace Product.Application.UseCases.GetByNameProduct;

public sealed record class GetByNameProductsQuery(string Name) : IQuery<List<ProductDto>>
{

}
