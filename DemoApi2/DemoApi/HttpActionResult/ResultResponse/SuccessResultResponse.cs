using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.HttpActionResult.ResultResponse
{
    public class SuccessResultResponse : BaseResultResponse
    {
        public object Data { get; set; }
    }
}