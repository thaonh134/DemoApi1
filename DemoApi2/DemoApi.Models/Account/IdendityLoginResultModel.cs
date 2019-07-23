using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Account
{
    public class IdendityLoginResultModel
    {
        public ClaimsIdentity Identity { get; set; }
        public ClaimsIdentity OAuthIdentity { get; set; }
        public ClaimsIdentity CookiesIdentity { get; set; }
    }
}
