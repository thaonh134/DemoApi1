using AutoMapper;
using DemoApi.Common.Exceptions;
using DemoApi.Common.Model;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories;
using DemoApi.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DemoApi.APIFilter
{
    public class ValidateStatusUserAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count() > 0)
            {
                return;
            }
            var context = HttpContext.Current;
            #region manager  user
            if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)context.User.Identity;
                var PrimarySid = identity.FindFirst(ClaimTypes.PrimarySid);
                if (PrimarySid == null)
                {
                    throw new UnAuthorizeException("AUTH_0003", "Phiên truy cập không hợp lệ");
                }
                test15_api_everEntities dbContext = new test15_api_everEntities();
                IAspNetUserRepository _aspNetUserRepository = new AspNetUserRepository(dbContext);
                var user = _aspNetUserRepository.Get(x => x.Id == PrimarySid.Value);

                if (user == null)
                {
                    throw new UnAuthorizeException("AUTH_0003", "Phiên truy cập không hợp lệ");
                }
                ValidBlock(user);

                if ((HttpContext.Current.Items == null || HttpContext.Current.Items["UserObjects"] == null))
                    HttpContext.Current.Items["UserObjects"] = new AspNetUserCommon();
                HttpContext.Current.Items["UserObjects"] = Mapper.Map<AspNetUserCommon>(user);
                dbContext.Dispose();
                //Save User to httpcontext
            }
            #endregion


            return;
        }

        public void ValidBlock(AspNetUser curruser)
        {
            //if (
            //    ((curruser.BlockType == (int)BlockType.Day1 || curruser.BlockType == (int)BlockType.Day7) && curruser.BlockExpired.Value > DateTime.Now)
            //    ||
            //    (curruser.BlockType == (int)BlockType.DayForever)
            //    )
            //{
            //    var blockTime = "";
            //    if (curruser.BlockType == (int)BlockType.Day1) blockTime = UserResourceMessage.Block_1_Day;
            //    else if (curruser.BlockType == (int)BlockType.Day7) blockTime = UserResourceMessage.Block_7_Days;
            //    else if (curruser.BlockType == (int)BlockType.DayForever) blockTime = "";
            //    throw new VOVUnAuthorizeException(nameof(CommonResourceMessage.AUTH_0002), CommonResourceMessage.AUTH_0002 + blockTime);
            //}
        }

    }

}