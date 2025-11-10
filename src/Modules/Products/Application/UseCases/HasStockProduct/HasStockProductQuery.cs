using MediatR;

namespace Application.UseCases.HasStockProduct;

public sealed record class HasStockProductQuery(Guid Id) : IRequest<HasStockDto>
{

}
