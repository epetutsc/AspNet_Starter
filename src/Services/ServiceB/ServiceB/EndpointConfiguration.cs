using EndpointConfiguration.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ServiceB
{
    internal class EndpointConfiguration : IConfigureEndpoints
    {
        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/B", async ctx => await ctx.Response.WriteAsync("SERVICE B"));
        }
    }
}
