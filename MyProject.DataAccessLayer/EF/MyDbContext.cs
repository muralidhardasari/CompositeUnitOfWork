using MyProject.DataAccessLayer.EF;
using MyProject.Domain;
using MyProject.Infrastructure.EntityFrameworkContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer
{
    public class MyDbContext : DbContext, IDbContext
    {
        public MyDbContext()
        {
            //Don't change the db when the model changes.
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext(DbConnection dbConnection, bool isReadonlyConnection) : base(dbConnection, false)
        {
            Database.SetInitializer<MyDbContext>(null);

            IsReadonlyConnection = isReadonlyConnection;
        }

        public bool TransactionSet { get; set; }
        public bool IsReadonlyConnection { get; set; }
        public IDbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new StudentConfiguration());
        }
    }
}
