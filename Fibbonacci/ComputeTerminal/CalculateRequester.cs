using MassTransit;
using RabbitMQ.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComputeTerminal
{
    class CalculateRequester : ICalculateRequester
    {
        private readonly IRestClient restClient;

        public CalculateRequester()
        {
            this.restClient = new RestClient("http://localhost:25491");
        }
        public void Send(int number)
        {
            var restRequest = new RestRequest("/api/ReceiveNumber/post", Method.POST);
            restRequest.AddParameter("application/json; charset=utf-8", string.Concat(number.ToString()), ParameterType.RequestBody);
            restClient.Execute(restRequest);
        }
    }
}
