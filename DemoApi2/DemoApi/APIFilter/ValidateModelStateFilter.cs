using DemoApi.Common.Exceptions;
using DemoApi.Helper;
using DemoApi.HttpActionResult.ResultResponse;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DemoApi.APIFilter
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                throw new BaseApiException("ModelState_invalid", String.Join(" ", errors.ToArray()));

            }
            //actionContext.Response = actionContext.Request
            //     .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
            return;

            var actionArguments = GetTheActionArguments(actionContext);

            if (actionArguments == null)
            {
                return;
            }

            foreach (var actionArgument in actionArguments)
            {
                if (actionArgument.Value == null)
                {
                    var actionArgumentDescriptor = GetActionArgumentDescriptor(actionContext, actionArgument.Key);

                    if (actionArgumentDescriptor.IsOptional)
                    {
                        continue;
                    }

                    var validator = GetValidatorForActionArgumentType(actionContext, actionArgumentDescriptor.ParameterType);

                    if (validator == null)
                    {
                        continue;
                    }

                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);

                    return;
                }
                else
                {
                    //var validator = actionContext.Request.GetDependencyScope().GetService(typeof(AbstractValidator<>).MakeGenericType(actionArgument.Value.GetType())) as IValidator;
                    var validator = GetValidatorForActionArgument(actionContext, actionArgument.Value);

                    if (validator == null)
                    {
                        continue;
                    }

                    var validationResult = validator.Validate(actionArgument.Value);

                    if (validationResult.IsValid)
                    {
                        continue;
                    }

                    WriteErrorsToModelState(validationResult, actionContext);

                    return;
                }
            }
        }

        private static IEnumerable<KeyValuePair<string, object>> GetTheActionArguments(HttpActionContext actionContext)
        {
            return actionContext.ActionArguments
                .Select(argument => argument);
        }

        private static HttpParameterDescriptor GetActionArgumentDescriptor(HttpActionContext actionContext, string actionArgumentName)
        {
            return actionContext.ActionDescriptor
                .GetParameters()
                .SingleOrDefault(prm => prm.ParameterName == actionArgumentName);
        }

        private IValidator GetValidatorForActionArgument(HttpActionContext actionContext, object actionArgument) =>
            GetValidatorForActionArgumentType(actionContext, actionArgument.GetType());

        private IValidator GetValidatorForActionArgumentType(HttpActionContext actionContext, Type actionArgument)
        {
            var attribute = actionArgument.GetCustomAttributes(typeof(ValidatorAttribute), true).FirstOrDefault() as ValidatorAttribute;
            if (attribute != null)
            {
                return CastleHelper.Resolver.GetService(attribute.ValidatorType) as IValidator;
                //return CastleHelper.Resolver.GetService(typeof(IValidator<>).MakeGenericType(actionArgument)) as IValidator;
            }
            return null;
        }

        private static void WriteErrorsToModelState(ValidationResult validationResults, HttpActionContext actionContext)
        {
            List<ErrorResponsePackage> errors = validationResults.GetErrorResponsePackages();
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new ErrorResultResponse
                {
                    Errors = errors,
                    Message = ""
                });
        }
    }
}