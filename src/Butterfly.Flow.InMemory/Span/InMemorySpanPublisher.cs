using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.Protocol;

namespace Butterfly.Flow.InMemory
{
    public class InMemorySpanPublisher : ISpanPublisher
    {
        private readonly IBlockingQueue<Span> _blockingQueue;

        public InMemorySpanPublisher(IBlockingQueue<Span> blockingQueue)
        {
            _blockingQueue = blockingQueue ?? throw new ArgumentNullException(nameof(blockingQueue));
        }

        public Task PostAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            if (_blockingQueue.IsActived && !cancellationToken.IsCancellationRequested)
            {
                _blockingQueue.Enqueue(spans);
            }

            return TaskUtils.FailCompletedTask;
        }
    }
}