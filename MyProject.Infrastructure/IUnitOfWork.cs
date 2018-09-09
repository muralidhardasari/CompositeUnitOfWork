using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Flushes content of unit of work to the underlying data storage. Causes unsaved
        /// entities to be written to the data storage and IDs to be filled in.
        /// </summary>
        void Flush();

        /// <summary>
        /// Inserts entity to the storage.
        /// </summary>
        void Insert<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Updates entity in the storage.
        /// </summary>
        void Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Deletes entity in the storage.
        /// </summary>
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        IQueryable<TEntity> Table<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets entity from the storage by it's Id.
        /// </summary>
        TEntity GetById<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class;

        /// <summary>
        /// Gets all entities of the type from the storage. 
        /// </summary>
        IList<TEntity> GetAll<TEntity>() where TEntity : class;

        /// <summary>
        /// Where clause
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IList<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        void Commit();

        /// <summary>
        /// Gets all entities of the type from the storage. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetAllQueryable<TEntity>() where TEntity : class;

        /// <summary>
        /// Where clause
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> WhereQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
