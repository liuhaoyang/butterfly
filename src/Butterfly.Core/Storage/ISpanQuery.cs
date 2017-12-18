using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Storage
{
    public interface ISpanQuery
    {
        Task<IEnumerable<Span>> GetSpans();

        Task<IEnumerable<Span>> GetTrace(string traceId);
    }
}