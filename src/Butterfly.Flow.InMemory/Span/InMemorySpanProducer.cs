using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;

namespace Butterfly.Flow.InMemory
{
    public class InMemorySpanProducer : ISpanProducer
    {
        private readonly IBlockingQueue<Span> _blockingQueue;

        public InMemorySpanProducer(IBlockingQueue<Span> blockingQueue)
        {
            _blockingQueue = blockingQueue ?? throw new ArgumentNullException(nameof(blockingQueue));
        }

        public Task PostAsync(IEnumerable<Span> spans, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_blockingQueue.IsActived && !cancellationToken.IsCancellationRequested)
            {
                _blockingQueue.Enqueue(spans);
            }

            return TaskUtils.FailCompletedTask;
        }
    }
}