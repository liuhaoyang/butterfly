using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Flow
{
    public interface ISpanProducer
    {
        Task PostAsync(IEnumerable<Span> spans, CancellationToken cancellationToken = default(CancellationToken));
    }
}