using DependencyInjection.Contracts;
using Kernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AssemblyInitializer.InitializeClassesImplementing<IConfigureServices>(baseDirectory, instance =>
            {
                instance.ConfigureServices(serviceCollection, configuration);
            });
        }
    }
}
