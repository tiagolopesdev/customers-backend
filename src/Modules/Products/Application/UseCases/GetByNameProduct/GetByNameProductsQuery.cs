using Application.Shared.Dtos;
using BlockApplication.Contracts.CommandQuery;

namespace Application.UseCases.GetByNameProduct;

public sealed record class GetByNameProductsQuery(string Name) : IQuery<List<ProductDto>>
{

}
