using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.UnitTests.TestClasses
{
    internal class TestClassA
    {
        public TestClassB _testClassB { get; private set; }
        public TestClassC _testClassC { get; private set; }

        public TestClassA()
        {
            _testClassB = new TestClassB();
            _testClassC = new TestClassC();
        }

        public TestClassA(TestClassB testClassB, TestClassC testClassC)
        {
            _testClassB = testClassB;
            _testClassC = testClassC;
        }
    }
}