
namespace Application.UseCases.Update
{
    public sealed record class UpdateStockItemModel(Guid ProductId, int Quantity)
    {
    }
}
