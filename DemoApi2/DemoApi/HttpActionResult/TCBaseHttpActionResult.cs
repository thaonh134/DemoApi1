using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DemoApi.HttpActionResult
{
    public class TCBaseHttpActionResult
    {
        protected HttpRequestMessage _request;
        protected string _message;
        protected string _code;
        protected object _data;

        public TCBaseHttpActionResult(HttpRequestMessage request, string code, string message)
        {
            _request = request;
            _code = code;
            _message = message;
        }

        public TCBaseHttpActionResult(HttpRequestMessage request, object data, string message)
        {
            _request = request;
            _data = data;
            _message = message;
        }
    }
}