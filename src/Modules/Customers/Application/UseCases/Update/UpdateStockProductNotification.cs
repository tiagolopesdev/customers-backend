using BlockApplication.Contracts.Notification;

namespace Application.UseCases.Update
{
    public sealed record class UpdateStockProductNotification(List<UpdateBuyRequest> Buy) : INotificationEvent
    {

    }
}
