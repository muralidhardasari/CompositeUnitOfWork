using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public interface IUnitOfWorkFactory : IDisposable
    {
        /// <summary>
        /// Begins unit of work.
        /// </summary>
        IUnitOfWork BeginUnitOfWork(SqlConnectionType connectiontype, SqlIsolationLevel isolationLevel, string featureName);
    }
}
