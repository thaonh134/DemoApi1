using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Account
{
    public class LoginModel
    {
        //public string Otp { get; set; }
        public string TypeDevice { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthAuthenticationType { get; set; }
    }
}
