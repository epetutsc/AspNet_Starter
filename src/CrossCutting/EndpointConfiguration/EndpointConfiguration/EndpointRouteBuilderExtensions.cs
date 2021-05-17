using System;
using AssemblyLoading;
using EndpointConfiguration.Contracts;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EndpointConfiguration
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IEndpointRouteBuilder endpoints, ILogger? logger = null)
        {
            AssemblyLoader
                .For(AppDomain.CurrentDomain.BaseDirectory)
                .UseInstanceOfType<IConfigureEndpoints>(instance =>
                {
                    logger?.LogInformation($"configure endpoints from {instance.GetType().Assembly.GetName().Name}");
                    instance.ConfigureEndpoints(endpoints);
                });
        }
    }
}
