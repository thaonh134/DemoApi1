using DemoApi.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoApi.Common.Helper
{
    public static class CurrentUserIdentityClaimHelper
    {
        private static AspNetUserCommon GetUserObjects()
        {
            var user = new AspNetUserCommon();
            if ((HttpContext.Current.Items == null || HttpContext.Current.Items["UserObjects"] == null))
                HttpContext.Current.Items["UserObjects"] = new AspNetUserCommon();
            user = (AspNetUserCommon)HttpContext.Current.Items["UserObjects"];
            return user;
        }

        public static string MobilePhone
        {
            get
            {
                return GetUserObjects().PhoneNumber;
            }
        }

        public static string Email
        {
            get
            {
                return GetUserObjects().Email;
            }
        }

        //public static string Role
        //{
        //    get
        //    {
        //        return GetUserObjects().RoleType.ToString();
        //    }
        //}

        public static string UserName
        {
            get
            {
                return GetUserObjects().UserName;
            }
        }

        public static string UserId
        {
            get
            {
                return GetUserObjects().Id;
            }
        }
        //public static string PriorityLevel
        //{
        //    get
        //    {
        //        return ((int)GetUserObjects().PriorityLevel).ToString();
        //    }
        //}
    }
}
