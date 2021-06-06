using Microsoft.AspNetCore.Routing;

namespace Main.Web.Tests.Endpoints
{
    public interface IDfaMatcherBuilder
    {
        void AddEndpoint(RouteEndpoint endpoint);
        object BuildDfaTree(bool includeLabel);
    }
}
