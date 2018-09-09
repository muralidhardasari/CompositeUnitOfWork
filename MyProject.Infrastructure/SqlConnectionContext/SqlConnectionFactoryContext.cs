using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public class SqlConnectionFactoryContext : IUnitOfWorkFactory
    {
        public IUnitOfWork BeginUnitOfWork(SqlConnectionType connectionType, SqlIsolationLevel isolationLevel, string feature)
        {
            return new SqlConnectionContext(connectionType, isolationLevel);
        }

        public void Dispose()
        {
        }
    }
}
