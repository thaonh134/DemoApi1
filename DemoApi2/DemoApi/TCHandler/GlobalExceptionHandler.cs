using DemoApi.Common.Exceptions;
using DemoApi.HttpActionResult;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace DemoApi.TCHandler
{
    /// <summary>
    /// Only get exception on http of apiController
    /// </summary>
    public class GlobalAPIExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            if (exception is System.NotImplementedException)
            {
                context.Result = new TCErrorHttpActionResult(context.Request, "Not_implemenet", "Chưa triển khai tính năng này");
            }
            else if (exception is UnAuthorizeException)
            {
                var TCException = exception as BaseApiException;
                context.Result = new TCErrorHttpActionResult(context.Request, HttpStatusCode.Unauthorized, TCException.VOVErrorCode, TCException.VOVErrorMessage);
            }
            else if (exception is BaseApiException)
            {
                var TCException = exception as BaseApiException;
                context.Result = new TCErrorHttpActionResult(context.Request, TCException.VOVErrorCode, TCException.VOVErrorMessage);
            }
            else if (exception is DbUpdateException)
            {
                context.Result = new TCErrorHttpActionResult(context.Request, "Internal_server_error", exception.ToString());
            }
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
}