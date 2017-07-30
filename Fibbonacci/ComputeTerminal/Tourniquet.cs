using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComputeTerminal
{
    class Tourniquet : ITourniquet
    {
        private readonly Semaphore semaphore;

        public Tourniquet(int countParallelCalls)
        {
            Logger.Log.Debug("create semaphore");
            semaphore = new Semaphore(countParallelCalls, countParallelCalls);
        }

        public bool Enter()
        {
            Logger.Log.Debug("enter semaphore");
            return semaphore.WaitOne();
        }

        public void Exit()
        {
            Logger.Log.Debug("exit semaphore");
            semaphore.Release();
        }
    }
}
