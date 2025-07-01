using Application.Contracts.Query;

namespace Application.UseCases.HasStockProduct;

public sealed record class HasStockProductQuery(Guid Id) : IQuery<HasStockDto>
{

}
