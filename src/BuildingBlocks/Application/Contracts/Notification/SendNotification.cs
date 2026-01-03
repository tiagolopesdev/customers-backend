using MediatR;

namespace BlockApplication.Contracts.Notification
{
    public class SendNotification : ISendNotification
    {
        private readonly IPublisher _mediator;

        public SendNotification(IPublisher mediator)
        {
            _mediator = mediator;
        }

        Task ISendNotification.Publish<T>(T notification, CancellationToken cancellationToken)
        {
            _mediator.Publish(notification, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
