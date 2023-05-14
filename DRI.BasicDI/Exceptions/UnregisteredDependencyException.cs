using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI.Exceptions
{
    public sealed class UnregisteredDependencyException : Exception
    {
        public UnregisteredDependencyException(string message) : base(message) { }
    }
}