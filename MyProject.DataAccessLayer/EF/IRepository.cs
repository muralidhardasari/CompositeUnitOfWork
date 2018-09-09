using MyProject.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.EF
{
    public interface IRepository<in TKey, TEntity> : IDisposable where TEntity : Entity<TKey>
    {
        TEntity GetById(TKey key);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> QueryIncluding(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
