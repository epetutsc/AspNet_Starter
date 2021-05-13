using EndpointConfiguration.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ServiceC
{
    internal class EndpointConfiguration : IConfigureEndpoints
    {
        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/C", async ctx => await ctx.Response.WriteAsync("SERVICE C"));
        }
    }
}
