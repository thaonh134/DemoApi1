using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Users.UserManagerModel
{
    class UserManagerModel
    {
    }
    public class RegisterUserModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string ResourceUrl { get; set; }
        public List<int> AreaIds { get; set; }
        public List<int> AuthRoleIds { get; set; }

    }
    public class EditUserInforModel
    {
        //public string Id { get; set; }
        //public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string ResourceUrl { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
    }
}
