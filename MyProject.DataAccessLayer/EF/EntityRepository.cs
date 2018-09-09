using MyProject.Infrastructure;
using MyProject.Infrastructure.Domain;
using MyProject.Infrastructure.EntityFrameworkContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.EF
{
    public abstract class EntityRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : Entity<TKey>
    {
        private readonly IUnitOfWork _unitOfWork;

        protected EntityRepository()
        {
            _unitOfWork = EntityUnitOfWork.GetOpenEntityUnitOfWork();
        }

        protected EntityRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public TEntity GetById(TKey key)
        {
            return _unitOfWork.GetById<TEntity, TKey>(key);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _unitOfWork.GetAllQueryable<TEntity>();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _unitOfWork.GetAllQueryable<TEntity>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            return _unitOfWork.WhereQueryable(filter);
        }

        public IQueryable<TEntity> QueryIncluding(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _unitOfWork.WhereQueryable(filter);
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        public void Add(TEntity entity)
        {
            _unitOfWork.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _unitOfWork.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _unitOfWork.Delete(entity);
        }
    }
}
