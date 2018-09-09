using System;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure.EntityFrameworkContext
{
    public interface IDbContext : IDisposable
    {
        Database Database { get; }
        bool TransactionSet { get; set; }
        bool IsReadonlyConnection { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }

    //public class MyDbContext : DbContext, IDbContext
    //{
    //    public MyDbContext()
    //    {
    //        //Don't change the db when the model changes.
    //        Database.SetInitializer<MyDbContext>(null);
    //    }

    //    public MyDbContext(DbConnection dbConnection, bool isReadonlyConnection) : base(dbConnection, false)
    //    {
    //        Database.SetInitializer<MyDbContext>(null);

    //        IsReadonlyConnection = isReadonlyConnection;
    //    }

    //    public bool TransactionSet { get; set; }
    //    public bool IsReadonlyConnection { get; set; }
    ////    public IDbSet<Eta> EtAs { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        //modelBuilder.Configurations.Add(new EtaConfiguration());

    //        //modelBuilder.Ignore<Truck>();
    //        //modelBuilder.Ignore<DeviceHeader>();
    //        //modelBuilder.Ignore<GpsCoordinate>();
    //    }
    //}
}
