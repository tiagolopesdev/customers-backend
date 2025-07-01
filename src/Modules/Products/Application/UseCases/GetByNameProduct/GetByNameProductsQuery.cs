using Application.Contracts.Query;
using Application.Shared.Dtos;

namespace Application.UseCases.GetByNameProduct;

public sealed record class GetByNameProductsQuery(string Name) : IQuery<List<ProductDto>>
{

}
