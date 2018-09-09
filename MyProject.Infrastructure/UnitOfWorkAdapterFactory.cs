using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public static class UnitOfWorkAdapterFactory
    {
        private static IUnitOfWorkFactory _currentUnitOfWorkFactory;

        /// <summary>
        /// Set the current type adapter factory
        /// </summary>
        /// <param name="unitOfWorkFactory">The adapter factory to set</param>
        public static void SetCurrent(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _currentUnitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Create a new type adapter from currect factory
        /// </summary>
        /// <returns>Created type adapter</returns>
        public static IUnitOfWork CreateAdapter(SqlConnectionType connectiontype, SqlIsolationLevel isolationLevel, string featureName)
        {
                return _currentUnitOfWorkFactory.BeginUnitOfWork(connectiontype, isolationLevel, null);
        }
    }
}
