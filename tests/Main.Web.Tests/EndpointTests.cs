using System.Text;
using FluentAssertions;
using Main.Web.Tests.Endpoints;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Main.Web.Tests
{
    /// <summary>
    /// https://andrewlock.net/detecting-duplicate-routes-in-aspnetcore/
    /// </summary>
    public class EndpointTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EndpointTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void ShouldNotHaveDuplicateEndpoints()
        {
            var detector = new DuplicateEndpointDetector(_factory.Services);
            var endpointData = _factory.Services.GetRequiredService<EndpointDataSource>();
            endpointData.Endpoints.Count.Should().BePositive();

            var duplicates = detector.GetDuplicateEndpoints(endpointData);

            var output = new StringBuilder();
            foreach (var keyValuePair in duplicates)
            {
                var allMatches = string.Join(", ", keyValuePair.Value);
                output.AppendLine($"Duplicate route: '{keyValuePair.Key}'. Matches: {allMatches}");
            }

            output.ToString().Should().BeEmpty();
        }
    }
}
