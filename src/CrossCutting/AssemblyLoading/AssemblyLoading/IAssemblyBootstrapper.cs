using System;

namespace AssemblyLoading
{
    public interface IAssemblyBootstrapper
    {
        void UseInstanceOfType<T>(Action<T> action, Func<Type, T?>? instanceFactory = null) where T : class;
    }
}