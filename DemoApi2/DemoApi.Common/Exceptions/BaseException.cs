using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Exceptions
{
    public class BaseApiException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public BaseApiException(string code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
    }
}
