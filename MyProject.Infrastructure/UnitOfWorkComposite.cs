using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public class UnitOfWorkComposite : IUnitOfWork
    {
        private readonly IList<IUnitOfWork> _unitsOfWork;

        public UnitOfWorkComposite(IList<IUnitOfWork> unitsOfWork)
        {
            _unitsOfWork = unitsOfWork;
        }

        public void Dispose()
        {
            foreach (var unitOfWork in _unitsOfWork)
            {
                unitOfWork.Dispose();
            }
        }

        public void Flush()
        {
            foreach (var unitOfWork in _unitsOfWork)
            {
                unitOfWork.Flush();
            }
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Table<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity GetById<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetAll<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            foreach (var unitOfWork in _unitsOfWork)
            {
                unitOfWork.Commit();
            }
        }

        public IQueryable<TEntity> GetAllQueryable<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> WhereQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
