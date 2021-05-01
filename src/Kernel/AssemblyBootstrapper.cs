using System;
using System.Linq;

namespace Kernel
{
    public class AssemblyBootstrapper
    {
        private readonly IAssemblyScanner _assemblyScanner;

        public AssemblyBootstrapper(IAssemblyScanner assemblyScanner)
        {
            _assemblyScanner = assemblyScanner;
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
