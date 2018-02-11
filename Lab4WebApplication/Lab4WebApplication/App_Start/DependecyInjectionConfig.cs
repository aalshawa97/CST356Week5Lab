using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Lab4WebApplication.Data;
using Lab4WebApplication.Repositories;
using Lab4WebApplication.Services;

namespace Lab4WebApplication.App_Start
{
    public class DependecyInjectionConfig
    {
        public static class DependencyInjectionConfig
        {
            public static void Register()
            {
                // Create the container as usual.
                var container = new Container();
                container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

                // Register your types, for instance:
                container.Register<IEntity, EntityRepository>(Lifestyle.Scoped);
                container.Register<AppDbContext, AppDbContext>(Lifestyle.Scoped);

                // This is an extension method from the integration package.
                container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

                container.Verify();

                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            }
        }
    }
}