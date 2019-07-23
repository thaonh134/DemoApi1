using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Exceptions
{
    public class BaseApiException : Exception
    {
        public string VOVErrorCode { get; set; }
        public string VOVErrorMessage { get; set; }

        public BaseApiException(string code, string message)
        {
            VOVErrorCode = code;
            VOVErrorMessage = message;
        }
    }
}
