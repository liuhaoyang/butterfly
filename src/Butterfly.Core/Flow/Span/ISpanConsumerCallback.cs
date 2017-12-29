using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;

namespace Butterfly.Flow
{
    public interface ISpanConsumerCallback
    {
        Task InvokeAsync(IEnumerable<Span> spans, CancellationToken cancellationToken);
    }
}