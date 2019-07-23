using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DemoApi.HttpActionResult.ResultResponse;

namespace DemoApi.HttpActionResult
{
    public class TCErrorHttpActionResult : TCBaseHttpActionResult, IHttpActionResult
    {
        private List<ErrorResponsePackage> _errors;
        private HttpStatusCode _statusCode = HttpStatusCode.BadRequest;
        public TCErrorHttpActionResult(HttpRequestMessage request, List<ErrorResponsePackage> errorCodes) : base(request, "", "")
        {
            _errors = errorCodes;
        }

        public TCErrorHttpActionResult(HttpRequestMessage request, string errorCode, string errorMessage) : base(request, "", "")
        {
            _errors = new List<ErrorResponsePackage>()
            {
                new ErrorResponsePackage(errorCode, errorMessage)
            };
        }

        public TCErrorHttpActionResult(HttpRequestMessage request, HttpStatusCode statusCode, string errorCode, string errorMessage) : base(request, "", "")
        {
            _statusCode = statusCode;
            _errors = new List<ErrorResponsePackage>()
            {
                new ErrorResponsePackage(errorCode, errorMessage)
            };
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(
                _statusCode,
                new ErrorResultResponse
                {
                    Errors = _errors,
                    Message = _message
                });

            return Task.FromResult(response);
        }
    }
}