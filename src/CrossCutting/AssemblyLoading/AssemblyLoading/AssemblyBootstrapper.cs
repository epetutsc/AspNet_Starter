using System;
using System.Linq;
using Kernel;

namespace AssemblyLoading
{
    internal class AssemblyBootstrapper : IAssemblyBootstrapper
    {
        private readonly AssemblyScanner _assemblyScanner;

        public AssemblyBootstrapper(AssemblyScanner assemblyScanner)
        {
            _assemblyScanner = assemblyScanner;
        }

        public static IAssemblyBootstrapper For(string baseDirectory)
        {
            var assemblyScanner = new AssemblyScanner(baseDirectory);
            return new AssemblyBootstrapper(assemblyScanner);
        }

        public void UseInstanceOfType<T>(Action<T> action, Func<Type, T?>? instanceFactory = null)
            where T : class
        {
            instanceFactory ??= DefaultCreateInstance;
            var instances = _assemblyScanner
                .GetClassesOfType<T>()
                .Select(type => instanceFactory(type)!)
                .Where(o => o != null);

            foreach (var instance in instances)
            {
                action(instance);
            }

            static T? DefaultCreateInstance(Type @class)
            {
                return CreateInstance<T>(@class);
            }
        }

        private static TInstance? CreateInstance<TInstance>(Type @class)
            where TInstance : class
        {
            try
            {
                return (TInstance?)Activator.CreateInstance(@class);
            }
            catch (Exception ex)
            {
                ex.Trace($"Could not create an instance of type {@class}");
                return null;
            }
        }
    }
}
