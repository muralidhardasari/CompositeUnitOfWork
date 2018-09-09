using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyProject.Infrastructure.EntityFrameworkContext
{
    public class EntityUnitOfWork : IUnitOfWork
    {
        public const string WebCallContextKey = "EntityUnitOfWork";

        private static readonly object LockObject = new object();
        private readonly IDbContext _context;

        public IDbContext Context
        {
            get { return _context; }
        }

        public EntityUnitOfWork(IDbContext context)
        {
            _context = context;
            lock (LockObject)
            {
                WebCallContext.SetData(WebCallContextKey, this);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            WebCallContext.FreeNamedDataSlot(WebCallContextKey, dispose: false);
        }

        public IQueryable<TEntity> Table<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public IList<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().AsExpandable().Where(predicate).ToList();//As Expandable needed for predicate builder (see http://www.albahari.com/nutshell/linqkit.aspx)
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Flush()
        {
            _context.SaveChanges();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public TEntity GetById<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IList<TEntity> GetAll<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public static IUnitOfWork GetOpenEntityUnitOfWork()
        {
            var unitOfWork = (EntityUnitOfWork)WebCallContext.GetData(WebCallContextKey);

            SqlConnectionContext.GetOpenSqlConnection();

            if (unitOfWork.Context.TransactionSet == false)//Workaround to check if transaction is opened.
            {
                unitOfWork.Context.Database.UseTransaction(SqlConnectionContext.GetTransaction());
                unitOfWork.Context.TransactionSet = true;
            }
            return unitOfWork;
        }

        public IQueryable<TEntity> GetAllQueryable<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> WhereQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Context.Set<TEntity>().Where(predicate);
        }
    }
}
