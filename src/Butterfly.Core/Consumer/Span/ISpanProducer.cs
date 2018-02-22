using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;

namespace Butterfly.Consumer
{
    public interface ISpanProducer
    {
        Task PostAsync(IEnumerable<Span> spans, CancellationToken cancellationToken = default(CancellationToken));
    }
}