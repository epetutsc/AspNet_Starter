using System;

namespace AssemblyLoading
{
    public interface IAssemblyBootstrapper
    {
        void UseInstanceOfType<T>(Action<T> action) where T : class;

        void UseInstanceOfType<T>(Action<T> action, Func<Type, T?>? instanceFactory) where T : class;
    }
}
