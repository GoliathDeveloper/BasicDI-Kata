using System;

namespace DRI.BasicDI
{
    public interface IContainer: IDisposable
    {
        void Dispose();
        T GetInstance<T>();
        void Register<T>(T instance = default);
        int Registrations();
        void Unregister<T>();
    }
}