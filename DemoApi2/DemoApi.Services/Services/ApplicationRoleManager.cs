using DemoApi.Common.Enums;
using DemoApi.Database.IdentityContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Services
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
        }

        public bool CheckUserInRole(ApplicationUser user, RoleType roleType)
        {
            bool result = false;
            foreach (var item in user.Roles.ToList())
            {
                string userRole = this.FindById(item.RoleId).Name;
                if (userRole == roleType.DescriptionAttr())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
