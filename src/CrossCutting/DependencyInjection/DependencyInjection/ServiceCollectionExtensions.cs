using System;
using DependencyInjection.Contracts;
using Kernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyScanner = new AssemblyScanner(baseDirectory);
            var bootstrapper = new AssemblyBootstrapper(assemblyScanner);

            bootstrapper.UseInstanceOfType<IConfigureServices>(instance =>
            {
                instance.ConfigureServices(serviceCollection, configuration);
            });
        }
    }
}
