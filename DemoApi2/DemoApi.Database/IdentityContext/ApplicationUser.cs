using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.IdentityContext
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ResourceUrl { get; set; }
        public string Description { get; set; }
        //public int BlockType { get; set; }
        //public Nullable<System.DateTime> BlockExpired { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public int Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        //public Nullable<int> CountryISO3166Numeric { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        // Concatenate the address info for display in tables and such:        
    }
}
