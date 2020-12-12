using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyGame.Infra.Data.Seeds
{
    public  abstract  class ForceDataSeed
    {
        public static  TEntity ForceId<TEntity>(Guid id, TEntity entity) where TEntity : Entity
        {
            var prop = entity.GetType().GetProperty("Id");
            prop.SetValue(entity, id);
            return entity;
        }

    }
    public abstract class Seed<T> where T : Entity
    {
        protected readonly ModelBuilder modelBuilder;
        protected readonly EntityTypeBuilder<T> Entity;
        public Seed(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
            this.Entity = modelBuilder.Entity<T>();

            Execute();
        }

        public void ForceDate(List<T> items)
        {
            SetDateTime(items, new DateTime(2020, 12, 13));
        }

        private static void SetDateTime(List<T> list, DateTime initialDateTime)
        {
            foreach (var item in list)
            {
                var prop = item.GetType().GetProperty("CreateDate");
                prop.SetValue(item, initialDateTime);
            }
        }
        public abstract void Execute();
    }
}
