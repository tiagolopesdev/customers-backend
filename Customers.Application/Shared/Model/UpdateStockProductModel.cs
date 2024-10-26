
namespace Customers.Application.Shared.Model
{
    public sealed record class UpdateStockProductModel(Guid ProductId, int Quantity)
    {
    }
}
