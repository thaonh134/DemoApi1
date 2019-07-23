using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DemoApi.Plumbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace DemoApi.WindsorInstallers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.
                    FromThisAssembly().
                    BasedOn<IController>().
                    If(c => c.Name.EndsWith("Controller")).
                    LifestyleScoped());

            container.Register(
               Classes.
                   FromThisAssembly().
                   BasedOn<IHttpController>().
                   If(c => c.Name.EndsWith("Controller")).
                   LifestyleScoped());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }

       
    }
}