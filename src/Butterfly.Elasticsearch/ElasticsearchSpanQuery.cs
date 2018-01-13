using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Butterfly.Storage.Query;

namespace Butterfly.Elasticsearch
{
    public class ElasticsearchSpanQuery : ISpanQuery
    {
        public Task<Span> GetSpan(string spanId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Trace> GetTrace(string traceId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PageResult<Trace>> GetTraces(TraceQuery traceQuery)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetServices()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Span>> GetSpanDependencies(DependencyQuery dependencyQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}