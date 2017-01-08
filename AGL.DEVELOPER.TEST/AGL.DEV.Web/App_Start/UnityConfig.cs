using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Configuration;
using AGL.DEV.Repository;
using System.Net.Http;
using AGL.DEV.Web.Services;

namespace AGL.DEV.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all components with the container here
            container.RegisterType<HttpClient>(new ContainerControlledLifetimeManager(), new InjectionFactory(x => new HttpClient()));            
            container.RegisterType<IRepository, WebServiceRepository>(
                new InjectionConstructor(ConfigurationManager.AppSettings["WebServiceUrl"], new ResolvedParameter<HttpClient>())
            );
            container.RegisterType<IService, Service>(new InjectionConstructor(new ResolvedParameter<IRepository>()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}