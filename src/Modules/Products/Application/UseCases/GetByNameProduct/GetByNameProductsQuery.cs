using Application.Shared.Dtos;
using MediatR;

namespace Application.UseCases.GetByNameProduct;

public sealed record class GetByNameProductsQuery(string Name) : IRequest<List<ProductDto>>
{

}
