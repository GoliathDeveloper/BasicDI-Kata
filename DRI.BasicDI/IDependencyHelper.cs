using System;
using System.Collections.Generic;
using System.Text;

namespace DRI.BasicDI
{
    interface IDependencyHelper
    {
        void CheckForCircularDependenciesAndDependenciesAreRegistered(Type typeToCheck, HashSet<Type> dependencyChain, Dictionary<Type, Func<object>> registeredTypes);
    }
}
