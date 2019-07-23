using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DemoApi.Database.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.WindsorInstallers
{
    public class DbContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<EverliveEntities>().LifestyleScoped());
        }

       
    }
}