using DRI.BasicDI.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace DRI.BasicDI
{
    public sealed class Container : IContainer
    {
        private Dictionary<Type, Func<object>> _registeredTypes = new Dictionary<Type, Func<object>>();
        DependencyHelper _dependencyHelper = new DependencyHelper();

        public void Register<T>(T instance = default)
        {
            if (_registeredTypes.ContainsKey(typeof(T)))
            {
                throw new AlreadyRegisteredException($"Dependency {typeof(T)} has already been registered");
            }
            if (instance == null)
            {
                _registeredTypes[typeof(T)] = () => Activator.CreateInstance(typeof(T), false);
            }
            else
            {
                _registeredTypes[typeof(T)] = () => instance;
            }
            //Check if types dependencies have been registered
            try
            {
                _dependencyHelper.CheckForCircularDependenciesAndDependenciesAreRegistered(typeof(T), new HashSet<Type>(), _registeredTypes);
            }
            //In this example we are handling the exceptions thrown in the helper and removing the register type
            //In a real world scenario I would not throw the exception again to ensure robustness and implement a logger
            catch (CircularDependencyException cde)
            {
                Unregister<T>();
                throw cde;
            }
            catch (UnregisteredDependencyException ude)
            {
                Unregister<T>();
                throw ude;
            }
        }

        public int Registrations()
        {
            return _registeredTypes.Count;
        }

        public T GetInstance<T>()
        {
            if (!_registeredTypes.TryGetValue(typeof(T), out Func<object> creatorFunc))
            {
                throw new UnregisteredDependencyException($"Type {typeof(T)} has not been registered.");
            }

            return (T)creatorFunc();
        }

        public void Unregister<T>()
        {
            _registeredTypes.Remove(typeof(T));
        }

        public void Dispose()
        {
            _registeredTypes = null;
        }
    }
}