using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.UnitTests.TestClasses
{
    internal class TestClassB
    {
        public TestClassB()
        {
            _testClassC = new TestClassC();
        }
        public TestClassB(TestClassC testClassC)
        {
            _testClassC = testClassC;
        }
        public TestClassC _testClassC { get; private set; }
    }
}