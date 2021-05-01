using System;
using EndpointConfiguration.Contracts;
using Kernel;
using Microsoft.AspNetCore.Routing;

namespace DependencyInjection
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IEndpointRouteBuilder endpoints)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyScanner = new AssemblyScanner(baseDirectory);
            var bootstrapper = new AssemblyBootstrapper(assemblyScanner);

            bootstrapper.UseInstanceOfType<IConfigureEndpoints>(instance =>
            {
                instance.ConfigureEndpoints(endpoints);
            });
        }
    }
}
