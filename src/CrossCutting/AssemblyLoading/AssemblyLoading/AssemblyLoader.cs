namespace AssemblyLoading
{
    public static class AssemblyLoader
    {
        public static IAssemblyBootstrapper For(string baseDirectory)
        {
            var assemblyScanner = new AssemblyScanner(baseDirectory);
            return new AssemblyBootstrapper(assemblyScanner);
        }
    }
}
