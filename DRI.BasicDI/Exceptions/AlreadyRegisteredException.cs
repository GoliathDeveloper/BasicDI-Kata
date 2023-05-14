using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.Exceptions
{
    public sealed class AlreadyRegisteredException : Exception
    {
        public AlreadyRegisteredException(string message) : base(message) { }
    }
}