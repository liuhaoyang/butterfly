using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage.Query;

namespace Butterfly.Storage
{
    public interface ISpanQuery
    {
        Task<Span> GetSpan(string spanId);

        Task<Trace> GetTrace(string traceId);

        Task<PageResult<Trace>> GetTraces(TraceQuery traceQuery);

        Task<IEnumerable<string>> GetServices();
    }
}