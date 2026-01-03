using BlockApplication.Contracts.Notification;

namespace Customer.Application.UseCases.UpdateStockProduct
{
    public sealed record class UpdateStockProductNotification(List<UpdatedStockModel> Buy) : INotificationEvent;
}
