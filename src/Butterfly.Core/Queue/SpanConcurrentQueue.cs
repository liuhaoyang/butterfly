using System.Collections.Concurrent;
using System.Collections.Generic;
using Butterfly.Protocol;

namespace Butterfly.Core
{
    public class SpanConcurrentQueue : ISpanQueue
    {
        private readonly ConcurrentQueue<IEnumerable<Span>> _concurrentQueue = new ConcurrentQueue<IEnumerable<Span>>();

        public void Enqueue(IEnumerable<Span> spans)
        {
            _concurrentQueue.Enqueue(spans);
        }

        public bool TryDequeue(out IEnumerable<Span> result)
        {
            return _concurrentQueue.TryDequeue(out result);
        }
    }
}