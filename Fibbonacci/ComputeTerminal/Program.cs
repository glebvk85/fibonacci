using Common;
using MassTransit;
using RabbitMQ.Client;
using RestSharp;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenPipes;
using System.Threading;

namespace ComputeTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.InitLogger();

            int n = 0;
            if (args.Length != 1 || !int.TryParse(args[0], out n))
                Console.WriteLine("Incorrect input parameters");
            else
            {
                var container = GetConfiguredContainer(n);
                var calc = container.GetInstance<Calculator>();
                calc.ComputeFibonacci();
            }
            Console.ReadLine();
        }

        static IContainer GetConfiguredContainer(int countParallelCalls)
        {
            var container = new Container();
            var tourniqet = new Tourniquet(countParallelCalls);
            var resultWriter = new ResultWriter();
            var consumer = new ResultValueConsumer(tourniqet, resultWriter);

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint(ConnectData.Channel, ec =>
                {
                    ec.Instance<ResultValueConsumer>(consumer);
                });
            });

            busControl.Start();

            container.Configure(r =>
            {
                r.For<ResultValueConsumer>().Use(consumer);
                r.For<IBusControl>().Use(busControl);
                r.For<Calculator>().Use<Calculator>();
                r.For<ICalculateRequester>().Use<CalculateRequester>();
                r.For<IRestClient>().Use<RestClient>();
                r.For<ITourniquet>().Use(tourniqet);
            });
            return container;
        }

    }
}
