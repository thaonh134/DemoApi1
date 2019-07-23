using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Enums
{
    public enum RoleType
    {
        [Description(RoleTypeDefineName.Admin)]
        Admin,
        [Description(RoleTypeDefineName.Member)]
        Member
    }

    public static class RoleTypeDefineName
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
    }

    public static class AuthRoleType
    {
        //public const int Admin = 1;
        //public const int CTV = 3;
        //public const int NomalUser = 5;
    }
}
