using BlockApplication.Contracts.CommandQuery;

namespace Product.Application.UseCases.HasStockProduct;

public sealed record class HasStockProductQuery(Guid Id) : IQuery<HasStockDto>
{

}
