using Flunt.Notifications;
using System;

namespace MyGame.Domain.Entities
{
    public abstract class Entity : Notifiable
    {
        public Guid Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public Entity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
