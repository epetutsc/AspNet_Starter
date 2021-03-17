using EndpointConfiguration.Contracts;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;

namespace SimpleService
{
    internal class EndpointConfiguration : IConfigureEndpoints
    {
        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            Debugger.Break();
        }
    }
}
