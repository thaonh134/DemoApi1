using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace DemoApi.HttpActionResult.ResultResponse
{
    public static class RequestHelper
    {
        public static HttpResponseMessage CreateErrorResultResponse(
            this HttpRequestMessage httpRequestMessage, 
            HttpStatusCode httpStatusCode,
            string errorCode,
            string errorMessage)
        {
            return httpRequestMessage.CreateResponse(httpStatusCode,
                new ErrorResultResponse
                {
                    Errors = new List<ErrorResponsePackage>()
                    {
                        new ErrorResponsePackage(errorCode, errorMessage)
                    },
                    Message = ""
                });
        }
    }
}