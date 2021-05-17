using System;
using AssemblyLoading;
using DependencyInjection.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AssemblyLoader
                .For(AppDomain.CurrentDomain.BaseDirectory)
                .UseInstanceOfType<IConfigureServices>(instance =>
                    instance.ConfigureServices(serviceCollection, configuration));
        }
    }
}
