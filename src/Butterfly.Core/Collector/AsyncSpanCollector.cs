using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Core
{
//    public class AsyncSpanCollector : ISpanCollector
//    {
//        private readonly ISpanQueue _spanQueue;
//
//        public AsyncSpanCollector(ISpanQueue spanQueue)
//        {
//            _spanQueue = spanQueue ?? throw new ArgumentNullException(nameof(spanQueue));
//        }
//
//        public Task Collect(IEnumerable<Span> spans, CancellationToken cancellationToken)
//        {
//            if (spans == null)
//            {
//                throw new ArgumentNullException(nameof(spans));
//            }
//
//            if (_spanQueue.IsActive && !cancellationToken.IsCancellationRequested)
//            {
//                return _spanQueue.Enqueue(spans, cancellationToken);
//            }
//
//            return TaskUtils.FailCompletedTask;
//        }
//    }
}