using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DemoApi.Database;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DemoApi.WindsorInstallers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<ApplicationUserManager>()
            //   .UsingFactoryMethod<ApplicationUserManager>(() => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).LifestylePerWebRequest());
            //container.Register(Component.For<ApplicationRoleManager>()
            //  .UsingFactoryMethod<ApplicationRoleManager>(() => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>()).LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyContaining<ReadOnlyDatabaseAttribute>().Where(e => e.Name.EndsWith("Repository"))
                            .WithServiceAllInterfaces()
                            .LifestyleScoped());

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<BaseUnitOfWork<EverliveEntities>>().LifestyleScoped());
        }
    }
    
}