using DemoApi.APIFilter;
using DemoApi.Helper;
using DemoApi.TCHandler;
using DemoApi.WindsorInstallers;
using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DemoApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new GlobalAPIExceptionHandler());
            //config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());

            config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(new ValidateStatusUserAction());
            //config.Filters.Add(new ValidateModelStateFilter());

            config.MessageHandlers.Add(new RequestResponseHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FluentValidationModelValidatorProvider.Configure(config, x => x.ValidatorFactory = new FluentValidatorCustomResolve(CastleHelper.Container.Kernel));
            config.DependencyResolver = CastleHelper.Resolver;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            var json = config.Formatters.JsonFormatter;

            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}