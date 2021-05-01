using System;
using System.Collections.Generic;

namespace Kernel
{
    public interface IAssemblyScanner
    {
        IEnumerable<Type> GetClassesOfType<T>();
    }
}