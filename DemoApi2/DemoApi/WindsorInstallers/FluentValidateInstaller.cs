using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DemoApi.Models.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.WindsorInstallers
{
    public class FluentValidateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<BaseModel>()
                .BasedOn(typeof(AbstractValidator<>))
                .WithServices()
                .LifestylePerWebRequest());
        }
    }
}