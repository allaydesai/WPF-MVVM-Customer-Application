//using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using ZzaDashboard.Services;


namespace ZzaDashboard
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            // centralized registration of any types that you want the container to be responsible for creating
            // whenever someone asks for an ICustomersRepository, give them a CustomersRepository as the concrete type
            // long name parameter in Unity means make it a singleton
            _container.RegisterType<ICustomersRepository, CustomersRepository>(new ContainerControlledLifetimeManager());
        }
        // a singleton instance of an IUnityContainer through a static property
        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
