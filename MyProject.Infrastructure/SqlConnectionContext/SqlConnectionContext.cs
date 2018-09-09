using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace MyProject.Infrastructure
{
    public class SqlConnectionContext : IUnitOfWork
    {
        public const string ConnectionKey = "SqlConnection";
        public const string TransactionKey = "SqlTransaction";

        //public const string ReadonlyConnectionKey = "ReadonlySqlConnection";
        //public const string ReadonlyTransactionKey = "SqlReadonlyTransaction";

        //private static readonly ILog Log = LogManager.GetLogger(typeof(SqlConnectionContext));
        private static readonly object LockObject = new object();

        private const string IsolationLevelKey = "SqlConnectionIsolationLevel";
        private const IsolationLevel DefaultIsolationLevel = IsolationLevel.ReadCommitted;

        public SqlConnectionContext(SqlConnectionType connectiontype, SqlIsolationLevel sqlIsolationLevel)
        {
            lock (LockObject)
            {
                if (WebCallContext.GetData(ConnectionKey) == null)
                {
                    WebCallContext.SetData(ConnectionKey, new  SqlConnection(ConnectionString));
                    WebCallContext.SetData(IsolationLevelKey, sqlIsolationLevel);
                }
                else
                {
                    throw new Exception("There is already a sql connection");
                }
            }
        }

        private static string ConnectionString
        {
            get
            {
                return WebCallContext.GetData("DBConnection") == null ? ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString : WebCallContext.GetData("DBConnection").ToString();
            }
        }

       public static SqlConnection GetDBConnection()
        {
            return SqlConnection(ConnectionKey);
        }

        public static SqlTransaction GetTransaction()
        {
            return Transaction(ConnectionKey, TransactionKey);
        }

        
        public void Dispose()
        {
            FreeTransaction();
        }

        public IList<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            var transaction = (SqlTransaction)WebCallContext.GetData(TransactionKey);
            if (transaction != null)
            {
               // DomainEvents.Raise(new BeforeCommit());
                transaction.Commit();
                //DomainEvents.Raise(new AfterCommit());
            }

        }

        public void Flush()
        {
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

        private static SqlTransaction Transaction(string connectionKey, string transactionKey)
        {
            var transaction = WebCallContext.GetData(transactionKey);
            if (transaction == null)
            {
                OpenSqlConnection(connectionKey, transactionKey);
                transaction = WebCallContext.GetData(transactionKey);
            }

            return (SqlTransaction)transaction;
        }

        private static SqlConnection SqlConnection(string connectionKey)
        {
            var sqlConnection = WebCallContext.GetData(connectionKey);
            if (sqlConnection == null)
            {
                throw new Exception("No sql connection available");
            }

            var connection = (SqlConnection)sqlConnection;
            return connection;
        }

        private static IsolationLevel SqlIsolationLevel(string isolationLevelKey)
        {
            if (string.IsNullOrEmpty(isolationLevelKey))
            {
                return DefaultIsolationLevel;
            }

            var isolationLevel = WebCallContext.GetData(isolationLevelKey);
            if (isolationLevel == null)
            {
                return DefaultIsolationLevel;
            }

            var sqlIsolationLevel = (SqlIsolationLevel)isolationLevel;
            switch (sqlIsolationLevel)
            {
                case Infrastructure.SqlIsolationLevel.ReadCommitted:
                    return IsolationLevel.ReadCommitted;
                case Infrastructure.SqlIsolationLevel.Snapshot:
                    return IsolationLevel.Snapshot;
                case Infrastructure.SqlIsolationLevel.ReadUncommitted:
                    return IsolationLevel.ReadUncommitted;
                default:
                    return DefaultIsolationLevel;
            }
        }

        private static SqlConnection OpenSqlConnection(string connectionKey, string transactionKey, string isolationLevelKey = null)
        {
            var connection = SqlConnection(connectionKey);
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (NullReferenceException exc)
                {
                    throw new Exception("OpenConnectionException", exc);
                }
                catch (InvalidOperationException exc)
                {
                    throw new Exception("OpenConnectionException", exc);
                }

                var isolationLevel = SqlIsolationLevel(isolationLevelKey);
                var transaction = connection.BeginTransaction(isolationLevel);
                WebCallContext.SetData(transactionKey, transaction);
            }

            return connection;
        }

        public static SqlConnection GetOpenSqlConnection()
        {
            return OpenSqlConnection(ConnectionKey, TransactionKey, IsolationLevelKey);
        }

        private static void FreeTransaction()
        {
            WebCallContext.FreeNamedDataSlot(TransactionKey);
            WebCallContext.FreeNamedDataSlot(ConnectionKey);
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
