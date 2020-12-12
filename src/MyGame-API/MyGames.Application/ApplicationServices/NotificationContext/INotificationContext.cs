using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Application.ApplicationServices.NotificationContext
{
    public interface INotificationContext
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }

        void AddNotification(Notification notification);
        void AddNotification(string key, string message);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
    }
}
