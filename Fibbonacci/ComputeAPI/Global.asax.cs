using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ComputeAPI
{
    public class WebApiApplication : HttpApplication
    {
        static IBusControl _busControl;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            _busControl = ConfigureBus();
            _busControl.Start();
        }

        public static IBus Bus
        {
            get { return _busControl; }
        }

        protected void Application_End()
        {
            _busControl.Stop(TimeSpan.FromSeconds(10)); ;
        }

        IBusControl ConfigureBus()
        {
            return MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }
    }
}
