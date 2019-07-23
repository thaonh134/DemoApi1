using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DemoApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.WindsorInstallers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)

        {
            container.Register(AllTypes.FromAssemblyNamed("DemoApi.Services")
                                  .Where(type => type.Name.EndsWith("Service"))
                                  .WithServiceAllInterfaces()
                                  .LifestyleScoped());

            container.Register(Component.For<IWindsorContainer>()
                .Instance(CastleHelper.Container)
                .LifestyleSingleton());

        }

    }
}