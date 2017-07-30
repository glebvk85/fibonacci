using Common;
using ComputeAPI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ComputeAPI.Controllers
{
    public class ReceiveNumberController : ApiController
    {
        public void Post([FromBody]string number)
        {
            var service = new FibbonacciService();
            WebApiApplication.Bus.Publish<ResultValue>(new
            {
                number = number,
                Result = service.Get(int.Parse(number)),
            });
        }
    }
}
