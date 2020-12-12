using Flunt.Notifications;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Application.ApplicationServices
{
    public abstract class ApplicationServiceBase<TEntity> where TEntity : Entity
    {
        protected readonly INotificationContext notificationContext;
        public ApplicationServiceBase(INotificationContext notificationContext)
        {
            this.notificationContext = notificationContext;
        }

        public bool Valid => !notificationContext.HasNotifications;
        public bool Invalid => notificationContext.HasNotifications;

        public void AddNotification(string key, string msg) => notificationContext.AddNotification(new Notification(key, msg));
        public void AddNotification(Notification notification) => notificationContext.AddNotification(notification);
        public void AddNotifications(ICollection<Notification> notifications) => notificationContext.AddNotifications(notifications);
        public IReadOnlyCollection<Notification> GetNotifications => notificationContext.Notifications;
        public virtual bool Validate(TEntity entity)
        {
            return Validate<TEntity>(entity);
        }
        public virtual bool Validate<TInnerEntity>(TInnerEntity entity) where TInnerEntity : Entity
        {
            if (entity.Invalid)
                AddNotifications(entity.Notifications.ToList());
            return entity.Valid;
        }

        public virtual bool Validate(params TEntity[] entities)
        {
            return Validate<TEntity>(entities);
        }

        public virtual bool Validate<TInnerEntity>(params TInnerEntity[] entities) where TInnerEntity : Entity
        {
            var allOk = new List<bool>(entities.Length);
            foreach (var entity in entities)
                allOk.Add(Validate(entity));

            return allOk.All(x => x);
        }
    }
}
