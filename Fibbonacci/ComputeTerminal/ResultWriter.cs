using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeTerminal
{
    class ResultWriter : IResultWriter
    {
        public void Save(string message)
        {
            Console.WriteLine(message);
        }
    }
}
