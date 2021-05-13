using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ImpromptuInterface;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Web.Tests.Endpoints
{
    /// <summary>
    /// source: https://andrewlock.net/detecting-duplicate-routes-in-aspnetcore/
    /// </summary>
    public class DuplicateEndpointDetector
    {
        private readonly IServiceProvider _services;
        public DuplicateEndpointDetector(IServiceProvider services)
        {
            _services = services;
        }

        public Dictionary<string, List<string>> GetDuplicateEndpoints(EndpointDataSource dataSource)
        {
            // Use ImpromptuInterface to create the required dependencies as shown in previous post
            var matcherBuilder = typeof(IEndpointSelectorPolicy)
                .Assembly
                .GetType("Microsoft.AspNetCore.Routing.Matching.DfaMatcherBuilder")!;

            matcherBuilder.Should().NotBeNull();

            // Build the list of endpoints used to build the graph
            var rawBuilder = _services.GetRequiredService(matcherBuilder);
            var builder = rawBuilder.ActLike<IDfaMatcherBuilder>();

            // This is the same logic as the original graph writer
            var endpoints = dataSource.Endpoints;
            for (var i = 0; i < endpoints.Count; i++)
            {
                if (endpoints[i] is RouteEndpoint endpoint && (endpoint.Metadata.GetMetadata<ISuppressMatchingMetadata>()?.SuppressMatching ?? false) == false)
                {
                    builder.AddEndpoint(endpoint);
                }
            }

            // Build the raw tree from the registered routes
            var rawTree = builder.BuildDfaTree(includeLabel: true);
            var tree = rawTree.ActLike<IDfaNode>();

            // Store a list of nodes that have already been visited 
            var visited = new Dictionary<IDfaNode, int>();

            // Store a dictionary of duplicates
            var duplicates = new Dictionary<string, List<string>>();

            // Build the graph by visiting each node, and calling LogDuplicates on each
            Visit(tree, LogDuplicates);

            // done
            return duplicates;

            void LogDuplicates(IDfaNode node)
            {
                // Add the node to the visited node dictionary if it isn't already
                // Generate a zero-based integer label for the node
                if (!visited.TryGetValue(node, out var label))
                {
                    label = visited.Count;
                    visited.Add(node, label);
                }

                // We can safely index into visited because this is a post-order traversal,
                // all of the children of this node are already in the dictionary.

                // Does this node have multiple matches?
                var matchCount = node.Matches?.Count ?? 0;
                if (matchCount > 1)
                {
                    // Add the node to the dictionary!
                    duplicates[node.Label] = node.Matches!.Select(x => x.DisplayName!).ToList();
                }
            }
        }

        // Identical to the version shown in the previous post
        static void Visit(IDfaNode node, Action<IDfaNode> visitor)
        {
            if (node.Literals?.Values != null)
            {
                foreach (var dictValue in node.Literals.Values)
                {
                    var value = dictValue.ActLike<IDfaNode>();
                    Visit(value, visitor);
                }
            }

            // Break cycles
            if (node.Parameters != null && !ReferenceEquals(node, node.Parameters))
            {
                var parameters = node.Parameters.ActLike<IDfaNode>();
                Visit(parameters, visitor);
            }

            // Break cycles
            if (node.CatchAll != null && !ReferenceEquals(node, node.CatchAll))
            {
                var catchAll = node.CatchAll.ActLike<IDfaNode>();
                Visit(catchAll, visitor);
            }

            if (node.PolicyEdges?.Values != null)
            {
                foreach (var dictValue in node.PolicyEdges.Values)
                {
                    var value = dictValue.ActLike<IDfaNode>();
                    Visit(value, visitor);
                }
            }

            visitor(node);
        }
    }
}
