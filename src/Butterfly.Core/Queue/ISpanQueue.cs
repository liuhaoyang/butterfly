using System.Collections.Generic;
using Butterfly.Protocol;

namespace Butterfly.Core
{
    public interface ISpanQueue
    {
        void Enqueue(IEnumerable<Span> spans);

        bool TryDequeue(out IEnumerable<Span> result);
    }
}