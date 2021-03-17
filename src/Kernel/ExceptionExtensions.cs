using System;

namespace Kernel
{
    public static class ExceptionExtensions
    {
        public static void Trace(this Exception exception, string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
            System.Diagnostics.Trace.WriteLine(exception);
        }
    }
}
