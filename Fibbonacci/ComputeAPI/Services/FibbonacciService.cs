using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputeAPI.Services
{
    class FibbonacciService
    {
        public int Get(int number)
        {
            var result = Calculate(number + 1);
            return result;
        }

        private int Calculate(int number)
        {
            return number > 1 ? Calculate(number - 1) + Calculate(number - 2) : number;
        }
    }
}