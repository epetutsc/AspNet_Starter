using Microsoft.AspNetCore.Routing;

namespace EndpointConfiguration.Contracts
{
    public interface IConfigureEndpoints
    {
        void ConfigureEndpoints(IEndpointRouteBuilder endpoints);
    }
}
