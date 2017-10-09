using System.Collections.Concurrent;

namespace Butterfly.APM.Core.OpenTracing
{
    public class SpanQueue : ISpanQueue
    {
        private readonly ConcurrentQueue<ISpan> concurrentQueue = new ConcurrentQueue<ISpan>();

        public void Enqueue(ISpan span)
        {
            concurrentQueue.Enqueue(span);
        }

        public bool TryDequeue(out ISpan span)
        {
            if (concurrentQueue.TryDequeue(out span))
            {
                if (span != null && span.SpanContext.Sampled)
                {
                    return true;
                }
            }

            return false;
        }
    }
}