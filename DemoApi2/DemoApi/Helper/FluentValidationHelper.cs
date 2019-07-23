using DemoApi.HttpActionResult.ResultResponse;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Helper
{
    public static class FluentValidationHelper
    {
        public static List<ErrorResponsePackage> GetErrorResponsePackages(this ValidationResult validationResults)
        {
            List<ErrorResponsePackage> errors = new List<ErrorResponsePackage>();
            foreach (var error in validationResults.Errors)
            {
                errors.Add(new ErrorResponsePackage(error.ErrorCode, error.ErrorMessage));
                //actionContext.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return errors;
        }
    }
}