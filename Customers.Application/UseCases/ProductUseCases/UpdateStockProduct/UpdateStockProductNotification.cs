
using Customers.Application.Shared.Model;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.UpdateStockProduct
{
    public sealed record class UpdateStockProductNotification(List<UpdateStockProductModel> Products) : INotification
    {
    }
}
