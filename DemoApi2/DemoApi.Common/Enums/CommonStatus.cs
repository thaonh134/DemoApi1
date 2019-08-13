using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Enums
{
    public enum LoginType
    {
        Internal = 0,
        Admin = 1,
        External = 2

    }
    public enum RelationShipStatus
    {
        Pending = 0,
        Accepted = 1,
        Declined = 2,
        Blocked = 3,

    }
}
