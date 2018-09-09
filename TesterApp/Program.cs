using MyProject.DataAccessLayer;
using MyProject.DataAccessLayer.EF;
using MyProject.DataAccessLayer.InstanceProvider;
using MyProject.Domain;
using MyProject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace TesterApp
{
   public class Program
    {
        public static void Main(string[] args)
        {
            UnityContainer container = new UnityContainer();
            Container.ConfigureContainer(container);

          //  UnitOfWorkAdapterFactory.SetCurrent(new UnitOfWorkComposite());
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkCompositeFactory>("global", new ContainerControlledLifetimeManager());
            UnitOfWorkAdapterFactory.SetCurrent(container.Resolve<IUnitOfWorkFactory>("global"));
            var unitOfWork = UnitOfWorkAdapterFactory.CreateAdapter(SqlConnectionType.UseWritableDatabase, SqlIsolationLevel.Default, Guid.NewGuid().ToString());
            Student stdObj = new Student { Name="Saathwik", Email="muralidhar.dasari@gmail.com", Marks=100 };

            var _studentRepository = container.Resolve<IRepository<int, Student>>();

            _studentRepository.Add(stdObj);

            unitOfWork.Commit();

        }
    }
}
