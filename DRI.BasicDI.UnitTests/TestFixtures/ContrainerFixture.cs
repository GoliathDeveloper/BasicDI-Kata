using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DRI.BasicDI.UnitTests.TestFixtures
{
    /// <summary>
    /// Test fixture used to initialize an instance of the container
    /// using IDisposable 
    /// </summary>
    public class ContrainerFixture: IDisposable
    {
        public Container container;
        //Setup
        public ContrainerFixture()
        {
            container = new Container();
        }

        //Teardown
        public void Dispose()
        {
            container.Dispose();
        }
    }
}
