using DemoApi.HttpActionResult.ResultResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoApi.APIFilter
{
    public class DemoAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {

            if (actionContext.Request.Headers.Authorization == null)
            {
                //Token null
                actionContext.Response = actionContext.Request.CreateErrorResultResponse(
                    HttpStatusCode.Unauthorized,
                    "AUTH_0001",
                    "Phiên truy cập không được phép()");
            }
            else if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                //Token wrong
                actionContext.Response = actionContext.Request.CreateErrorResultResponse(
                    HttpStatusCode.Unauthorized,
                    "AUTH_0003",
                    "Phiên truy cập không hợp lệ");
            }
            else if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                //role deni
                actionContext.Response = actionContext.Request.CreateErrorResultResponse(
                    HttpStatusCode.Unauthorized,
                    "AUTH_0004",
                    "Không đủ quyền truy cập");
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}