using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeTerminal
{
    interface IResultWriter
    {
        void Save(string message);
    }
}
