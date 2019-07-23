using System;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(DemoApi.Models.App_Start.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(DemoApi.Models.App_Start.WindsorActivator), "Shutdown")]

namespace DemoApi.Models.App_Start
{
    public static class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }
        
        public static void Shutdown()
        {
            if (bootstrapper != null)
                bootstrapper.Dispose();
        }
    }
}