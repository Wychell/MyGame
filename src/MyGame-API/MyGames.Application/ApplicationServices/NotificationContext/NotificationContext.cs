using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Application.ApplicationServices.NotificationContext
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();
        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }
        public void AddNotification(string key, string message) => _notifications.Add(new Notification(key, message));
        public void AddNotification(Notification notification) => _notifications.Add(notification);
        public void AddNotifications(IReadOnlyCollection<Notification> notifications) => _notifications.AddRange(notifications);
        public void AddNotifications(IList<Notification> notifications) => _notifications.AddRange(notifications);
        public void AddNotifications(ICollection<Notification> notifications) => _notifications.AddRange(notifications);
    }
}
