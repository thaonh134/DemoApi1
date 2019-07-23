using Castle.MicroKernel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.WindsorInstallers
{
    public class FluentValidatorCustomResolve : ValidatorFactoryBase
    {
        private readonly IKernel _kernel;

        public FluentValidatorCustomResolve(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _kernel.HasComponent(validatorType)
                         ? _kernel.Resolve<IValidator>(validatorType)
                         : null;
        }
    }
}