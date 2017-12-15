using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Core
{
    public interface ISpanConsumer
    {
        Task Accept(IEnumerable<Span> spans, CancellationToken cancellationToken);
    }
}