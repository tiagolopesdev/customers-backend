namespace Customer.Application.UseCases.UpdateStockProduct
{
    public sealed record class UpdatedStockModel(Guid ProductId, int Quantity)
    {
    }
}
