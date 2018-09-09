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
            //   var ecoReportEntityUnitOfWork = new EcoReportEntityUnitOfWorkFactory().BeginUnitOfWork(connectiontype, isolationLevel, null);
            //  var mongoUnitOfWork = new MongoFactoryContext().BeginUnitOfWork(connectiontype, isolationLevel, null);
            // The cache unit of work must be placed after the SQL unit of work. When the SQL transaction commit fails,
            // an exception is thrown and the cache is not updated. The keys remain locked, but the locks have a
            // relatively low time out value. After this time, the cache resumes normal operation for the affected keys.
            //   var cacheUnitOfWork = new CacheUnitOfWorkFactory().BeginUnitOfWork(connectiontype, isolationLevel, null);

            return new UnitOfWorkComposite(new List<IUnitOfWork>
            {
                sqlConnectionUnitOfWork,
                entityUnitOfWork
            //    ecoReportEntityUnitOfWork,
             //   cacheUnitOfWork,
              //  mongoUnitOfWork
            });
        }
    }
}
