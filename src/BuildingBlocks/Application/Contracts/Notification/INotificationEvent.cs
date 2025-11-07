using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockApplication.Contracts.Notification
{
    public interface INotificationEvent : INotification
    {
    }
    public interface INotificationEventHandler<T> : INotificationHandler<T> where T : INotificationEvent
    {
    }
}
