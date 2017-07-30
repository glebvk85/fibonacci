using Common;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComputeTerminal
{
    partial class Calculator
    {
        private readonly ICalculateRequester calculateRequester;
        private readonly ITourniquet tourniquet;
        private IBusControl busControl;
        public Calculator(ICalculateRequester calculateRequester,
            ITourniquet tourniquet, IBusControl busControl)
        {
            this.calculateRequester = calculateRequester;
            this.tourniquet = tourniquet;
            this.busControl = busControl;
        }

        public void ComputeFibonacci()
        {
            int i = 1;
            while (true)
            {
                if (tourniquet.Enter())
                {
                    Logger.Log.Info(string.Format("Start calculate number {0}", i));
                    calculateRequester.Send(i);
                }
                i++;
            }
        }
    }
}
