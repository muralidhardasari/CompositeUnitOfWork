using MyProject.Infrastructure;
using MyProject.Infrastructure.EntityFrameworkContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.EF
{
    public class EntityUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public void Dispose()
        {
        }

        public IUnitOfWork BeginUnitOfWork(SqlConnectionType connectiontype, SqlIsolationLevel isolationLevel, string feature)
        {
            SqlConnection sqlConnection = SqlConnectionContext.GetDBConnection();//Unopened
            var isReadonlyConnection = connectiontype == SqlConnectionType.UseReadOnlyDatabase;
            var myDbContext = new MyDbContext(sqlConnection, isReadonlyConnection);
            return new EntityUnitOfWork(myDbContext);
        }
    }
}
