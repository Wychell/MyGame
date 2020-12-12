using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyGame.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<TEntity> FindByIdAsync(Guid id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> AllAsync();
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task InsertAsync(TEntity TEntity);
        Task<bool> CommitAsync();
        Task UpdateAsync(TEntity TEntity);
    }
}
