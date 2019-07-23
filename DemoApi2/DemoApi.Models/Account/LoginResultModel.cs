using DemoApi.Database.IdentityContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Account
{
    public class LoginResultModel
    {
        public ApplicationUser User { get; set; }
        public IdendityLoginResultModel IdendityLoginResult { get; set; }
    }
}
