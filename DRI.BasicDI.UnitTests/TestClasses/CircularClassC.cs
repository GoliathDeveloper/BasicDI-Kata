using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.UnitTests.TestClasses
{
    internal class CircularClassC
    {
        public CircularClassC(CircularClassA circularClassA)
        {
            if (circularClassA == null) throw new ArgumentNullException();
        }
    }
}