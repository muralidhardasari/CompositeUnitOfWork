using MyProject.DataAccessLayer.EF;
using MyProject.Infrastructure;
using System.Collections.Generic;

namespace MyProject.DataAccessLayer
{
    public class UnitOfWorkCompositeFactory : IUnitOfWorkFactory
    {
        public void Dispose()
        {
        }

        public IUnitOfWork BeginUnitOfWork(SqlConnectionType connectiontype, SqlIsolationLevel isolationLevel, string feature)
        {
            var sqlConnectionUnitOfWork = new SqlConnectionFactoryContext().BeginUnitOfWork(connectiontype, isolationLevel, null);
            var entityUnitOfWork = new EntityUnitOfWorkFactory().BeginUnitOfWork(connectiontype, isolationLevel, null);

            return new UnitOfWorkComposite(new List<IUnitOfWork>
            {
                sqlConnectionUnitOfWork,
                entityUnitOfWork
            });
        }
    }
}
