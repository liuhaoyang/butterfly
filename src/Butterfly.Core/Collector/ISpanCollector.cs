using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Core
{
    public interface ISpanCollector
    {
        Task Collect(IEnumerable<Span> spans, CancellationToken cancellationToken);
    }
}