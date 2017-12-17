using System;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Flow.InMemory
{
    public class InMemorySpanConsumer : ISpanConsumer
    {
        private readonly IBlockingQueue<Span> _blockingQueue;

        public InMemorySpanConsumer(IBlockingQueue<Span> blockingQueue)
        {
            _blockingQueue = blockingQueue ?? throw new ArgumentNullException(nameof(blockingQueue));
        }

        public async Task AcceptAsync(ISpanConsumerCallback callback, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.FromCanceled(cancellationToken);
            }

            foreach (var spans in _blockingQueue.DequeueEnumerable())
            {
                if (!cancellationToken.IsCancellationRequested)
                    await callback.InvokeAsync(spans, cancellationToken);
            }
        }
    }
}