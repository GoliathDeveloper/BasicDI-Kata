using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.UnitTests.TestClasses
{
    internal class CircularClassB
    {
        //public CircularClassB(CircularClassC circularClassC)
        //{
        //    if (circularClassC == null) throw new ArgumentNullException();
        //}

        public CircularClassC CircularClassC { get; }

        public CircularClassB(CircularClassC circularClassC)
        {
            CircularClassC = circularClassC ?? throw new ArgumentNullException();
        }
    }
}