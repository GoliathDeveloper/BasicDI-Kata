using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRI.BasicDI.Exceptions;
using Microsoft.Extensions.DependencyModel;

namespace DRI.BasicDI
{
    public class DependencyHelper: IDependencyHelper
    {
        /// <summary>
        /// Recursive method to search for circular dependencies
        /// </summary>
        /// <param name="typeToCheck"></param>
        /// <param name="dependencyChain"></param>
        /// <param name="_registeredTypes"></param>
        /// <exception cref="CircularDependencyException"></exception>
        /// <exception cref="UnregisteredDependencyException"></exception>
        public void CheckForCircularDependenciesAndDependenciesAreRegistered(Type typeToCheck, HashSet<Type> dependencyChain, Dictionary<Type, Func<object>> _registeredTypes)
        {
            if (dependencyChain.Contains(typeToCheck))
            {
                throw new CircularDependencyException(string.Join(" -> ", dependencyChain) + " -> " + typeToCheck);
            }

            dependencyChain.Add(typeToCheck);

            //only looking for constructors taking a class
            var constrs = GetConstructorParameters(typeToCheck);
            if (constrs.Count == 1)
            {
                //Check each constructor parameter to see if they have been registered
                foreach (var para in constrs[0].GetParameters())
                {
                    CheckForCircularDependenciesAndDependenciesAreRegistered(para.ParameterType, new HashSet<Type>(dependencyChain), _registeredTypes);
                }
            }
            //for this example I wont be coding for multiple constructors would use else if (constrs.Count > 1)
            foreach (var t in dependencyChain)
            {
                if (!_registeredTypes.ContainsKey(t))
                {
                    _registeredTypes.Remove(typeToCheck);
                    throw new UnregisteredDependencyException($"Type {t} has not been registered.");
                }
            }
        }

        public static List<ConstructorInfo> GetConstructorParameters(Type type)
        {
            //only looking for constructors taking a class
            return type.GetConstructors().Where(c => c.GetParameters().Count() >= 1).ToList();
        }
    }
}