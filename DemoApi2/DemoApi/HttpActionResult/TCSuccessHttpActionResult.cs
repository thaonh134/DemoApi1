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
    public class TCSuccessHttpActionResult : TCBaseHttpActionResult, IHttpActionResult
    {
        public TCSuccessHttpActionResult(HttpRequestMessage request, object data) : base(request, data, string.Empty)
        {
        }

        public TCSuccessHttpActionResult(HttpRequestMessage request, object data, string message) : base(request, data, message)
        {
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(
                HttpStatusCode.OK,
                new SuccessResultResponse
                {
                    Data = _data,
                    Message = _message
                });

            return Task.FromResult(response);
        }
    }
}