using Common;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeTerminal
{
    class ResultValueConsumer : IConsumer<ResultValue>
    {
        private readonly ITourniquet tourniqet;
        private readonly IResultWriter resultWriter;
        public ResultValueConsumer(ITourniquet tourniqet, IResultWriter resultWriter)
        {
            this.tourniqet = tourniqet;
            this.resultWriter = resultWriter;
        }
        public Task Consume(ConsumeContext<ResultValue> context)
        {
            tourniqet.Exit();
            var message = string.Format("Result # {0} equal {1}", context.Message.Number, context.Message.Result);
            Logger.Log.Info(message);
            resultWriter.Save(message);
            return Task.FromResult(context);
        }
    }
}
