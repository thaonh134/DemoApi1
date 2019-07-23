using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Model
{
    public class AspNetUserCommon
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Active { get; set; }
        public string ResourceUrl { get; set; }
        //public int BlockType { get; set; }

        //public RoleType RoleType { get; set; }
        //public PriorityLevel PriorityLevel { get; set; }
        //public List<int> AreaIds { get; set; }
        //public List<int> AuthRoleIds { get; set; }

        //public Nullable<System.DateTime> BlockExpired { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public int Gender { get; set; }
        public Nullable<int> CountryISO3166Numeric { get; set; }
    }
}
