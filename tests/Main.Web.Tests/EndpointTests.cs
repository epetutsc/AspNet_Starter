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
        public void Main_Web_Should_Not_Have_Duplicate_Endpoints()
        {
            var detector = new DuplicateEndpointDetector(_factory.Services);
            var endpointDataSource = _factory.Services.GetRequiredService<EndpointDataSource>();

            GetErrorMessage(detector, endpointDataSource).Should().BeEmpty();
        }

        [Fact]
        public void Duplicate_Endpoints_Should_Raise_Error()
        {
            var detector = new DuplicateEndpointDetector(_factory.Services);
            var firstEndpoint = _factory.Services.GetRequiredService<EndpointDataSource>().Endpoints[0];
            var endpointData = new DefaultEndpointDataSource(firstEndpoint, firstEndpoint);

            GetErrorMessage(detector, endpointData).Should().NotBeEmpty();
        }

        private static string GetErrorMessage(DuplicateEndpointDetector detector, EndpointDataSource endpointData)
        {
            endpointData.Endpoints.Count.Should().BePositive();

            var duplicates = detector.GetDuplicateEndpoints(endpointData);

            var output = new StringBuilder();
            foreach (var keyValuePair in duplicates)
            {
                var allMatches = string.Join(", ", keyValuePair.Value);
                output.AppendLine($"Duplicate route: '{keyValuePair.Key}'. Matches: {allMatches}");
            }

            return output.ToString();
        }
    }
}
