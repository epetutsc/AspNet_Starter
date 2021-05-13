using System;
using EndpointConfiguration.Contracts;
using Kernel;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EndpointConfiguration
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IEndpointRouteBuilder endpoints, ILogger? logger = null)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyScanner = new AssemblyScanner(baseDirectory);
            var bootstrapper = new AssemblyBootstrapper(assemblyScanner);

            bootstrapper.UseInstanceOfType<IConfigureEndpoints>(instance =>
            {
                logger?.LogInformation($"configure endpoints from {instance.GetType().Assembly.GetName().Name}");
                instance.ConfigureEndpoints(endpoints);
            });
        }
    }
}
