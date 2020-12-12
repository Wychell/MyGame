using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using MyGame.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Infra.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        protected DbSet<TEntity> Set;
        private readonly MyGameContext Context;

        public RepositoryBase(MyGameContext myGameContext)
        {
            Set = myGameContext.Set<TEntity>();
            Context = myGameContext;
        }
        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Set.ToListAsync();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.AnyAsync(predicate);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Set.Remove(entity);
            Set.Attach(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(Guid id)
        {
            var TEntity = Set.Find(id);
            return DeleteAsync(TEntity);
        }

        public virtual Task<TEntity> FindByIdAsync(Guid id)
        {
            return Set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<bool> CommitAsync()
        {
            return (await Context.SaveChangesAsync()) > 0;
        }

        public virtual Task InsertAsync(TEntity TEntity)
        {
            return Set.AddAsync(TEntity).AsTask();
        }
        public virtual Task UpdateAsync(TEntity TEntity)
        {
            Set.Update(TEntity);
            return Task.CompletedTask;
        }

        public async virtual Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await Set.Where(predicate).ToListAsync();
            return data;
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.FirstOrDefaultAsync(predicate);
        }
    }
}
