using MyProject.DataAccessLayer.EF;
using MyProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace MyProject.DataAccessLayer.InstanceProvider
{
    public class Container
    {
        public static void ConfigureContainer(UnityContainer container)
        {
            RegisterRepositories(container);
            RegisterDal(container);
        }

        private static void RegisterDal(UnityContainer container)
        {
            //TODO: Add SQL DAL
        }

        private static void RegisterRepositories(UnityContainer container)
        {
            container.RegisterType<IRepository<int, Student>, StudentEntityRepository>(new InjectionConstructor());
        }
    }
}
