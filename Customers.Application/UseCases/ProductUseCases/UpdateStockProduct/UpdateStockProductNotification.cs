
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.UpdateStockProduct
{
    public sealed record class UpdateStockProductNotification(List<string> Name, List<int> Quantity) : INotification
    {
    }
}
