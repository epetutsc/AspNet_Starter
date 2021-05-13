using EndpointConfiguration.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ServiceA
{
    internal class EndpointConfiguration : IConfigureEndpoints
    {
        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/A", async ctx => await ctx.Response.WriteAsync("SERVICE A"));
        }
    }
}
