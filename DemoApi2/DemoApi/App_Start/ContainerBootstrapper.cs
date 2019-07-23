using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DemoApi.Helper;

namespace DemoApi.App_Start
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;

        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer();
            CastleHelper.Container = container;

            container.Install(FromAssembly.This());
            container.Install(FromAssembly.Named("DemoApi.Services"));
            container.Install(FromAssembly.Named("DemoApi.Models"));
            container.Install(FromAssembly.Named("DemoApi.Common"));

            CastleHelper.Init();
            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}