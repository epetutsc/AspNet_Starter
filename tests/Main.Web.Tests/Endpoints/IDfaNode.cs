using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Main.Web.Tests.Endpoints
{
    public interface IDfaNode
    {
        public string Label { get; set; }
        public List<Endpoint> Matches { get; }
        public IDictionary Literals { get; }
        public object Parameters { get; }
        public object CatchAll { get; }
        public IDictionary PolicyEdges { get; }
    }
}
