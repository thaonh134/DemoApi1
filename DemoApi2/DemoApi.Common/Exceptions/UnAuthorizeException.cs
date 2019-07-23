using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Exceptions
{
    public class UnAuthorizeException : BaseApiException
    {
        public UnAuthorizeException(string code, string message) : base(code, message)
        {
        }
    }
}
