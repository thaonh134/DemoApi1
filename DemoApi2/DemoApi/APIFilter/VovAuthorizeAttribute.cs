using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoApi.APIFilter
{
    public class VovAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {

            //if (actionContext.Request.Headers.Authorization == null)
            //{
            //    //Token null
            //    actionContext.Response = actionContext.Request.CreateErrorResultResponse(
            //        HttpStatusCode.Unauthorized,
            //        nameof(CommonResourceMessage.AUTH_0001),
            //        CommonResourceMessage.AUTH_0001);
            //}
            //else if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            //{
            //    //Token wrong
            //    actionContext.Response = actionContext.Request.CreateErrorResultResponse(
            //        HttpStatusCode.Unauthorized,
            //        nameof(CommonResourceMessage.AUTH_0003),
            //        CommonResourceMessage.AUTH_0003);
            //}
            //else if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            //{
            //    //role deni
            //    actionContext.Response = actionContext.Request.CreateErrorResultResponse(
            //        HttpStatusCode.Unauthorized,
            //        nameof(CommonResourceMessage.AUTH_0004),
            //        CommonResourceMessage.AUTH_0004);
            //}
            //else
            //{
            //    base.HandleUnauthorizedRequest(actionContext);
            //}
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}