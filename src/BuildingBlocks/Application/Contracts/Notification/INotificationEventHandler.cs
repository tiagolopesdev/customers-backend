using MediatR;

namespace BlockApplication.Contracts.Notification
{
    public interface INotificationEventHandler<T> : INotificationHandler<T> where T : INotificationEvent
    {
    }
}
