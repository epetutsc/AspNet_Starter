using EndpointConfiguration.Contracts;
using Kernel;
using Microsoft.AspNetCore.Routing;
using System;

namespace DependencyInjection
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void AddFromAssembliesInCurrentDirectory(this IEndpointRouteBuilder endpoints)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AssemblyInitializer.InitializeClassesImplementing<IConfigureEndpoints>(baseDirectory, instance =>
            {
                instance.ConfigureEndpoints(endpoints);
            });
        }
    }
}
