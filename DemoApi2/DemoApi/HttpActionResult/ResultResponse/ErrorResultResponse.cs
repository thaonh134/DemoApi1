using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace DemoApi.HttpActionResult.ResultResponse
{
    public class ErrorResultResponse : BaseResultResponse
    {
        public List<ErrorResponsePackage> Errors { get; set; }
    }
    public class ErrorResponsePackage
    {
        public ErrorResponsePackage()
        {
        }

        public ErrorResponsePackage(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}