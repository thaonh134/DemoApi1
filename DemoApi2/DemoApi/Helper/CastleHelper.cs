using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoApi.WindsorInstallers;

namespace DemoApi.Helper
{
    public static class CastleHelper
    {
        private static WindsorContainer _container;
        private static WindsorHttpDependencyResolver _resolver;

        public static void Init()
        {
            _resolver = new WindsorHttpDependencyResolver(Container);
        }

        public static WindsorContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }
        public static WindsorHttpDependencyResolver Resolver
        {
            get
            {
                return _resolver;
            }
        }

        public static IValidator GetValidatorForActionArgumentType(Type actionArgument)
        {
            var attribute = actionArgument.GetCustomAttributes(typeof(ValidatorAttribute), true).FirstOrDefault() as ValidatorAttribute;
            if (attribute != null)
            {
                return CastleHelper.Resolver.GetService(attribute.ValidatorType) as IValidator;
            }
            return null;
        }
    }
}