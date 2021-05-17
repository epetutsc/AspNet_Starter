using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Kernel;

namespace AssemblyLoading
{
    internal class AssemblyScanner
    {
        private readonly string _baseDirectory;

        public IEnumerable<string> AssemblyPaths => Directory
            .EnumerateFiles(_baseDirectory, "*.dll", SearchOption.TopDirectoryOnly);

        public IEnumerable<Assembly> Assemblies => AssemblyPaths
            .Select(TryLoadAssembly)
            .Where(assembly => assembly != null)!;

        public AssemblyScanner(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        private static Assembly? TryLoadAssembly(string path)
        {
            try
            {
                return Assembly.LoadFrom(path);
            }
            catch (Exception ex)
            {
                ex.Trace($"Could not load assembly {path}");
                return null;
            }
        }

        public IEnumerable<Type> GetClassesOfType<T>()
        {
            return Assemblies
                .SelectMany(assembly => assembly
                    .GetTypes()
                    .Where(type => type.IsClass && typeof(T).IsAssignableFrom(type)));
        }
    }
}
