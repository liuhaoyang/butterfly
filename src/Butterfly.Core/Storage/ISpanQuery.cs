using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.Protocol;
using Butterfly.Storage.Query;

namespace Butterfly.Storage
{
    public interface ISpanQuery
    {
        Task<IEnumerable<Span>> GetSpans();

        Task<Trace> GetTrace(string traceId);

        Task<PageResult<Trace>> GetTraces(TraceQuery traceQuery);

        Task<IEnumerable<string>> GetServices();
    }
}