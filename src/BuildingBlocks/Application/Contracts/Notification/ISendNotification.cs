using MediatR;

namespace BlockApplication.Contracts.Notification
{
    public interface ISendNotification
    {
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
            where TNotification : INotification;
    }
}
