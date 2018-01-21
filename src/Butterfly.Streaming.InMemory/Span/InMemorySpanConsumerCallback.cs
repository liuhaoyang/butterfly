using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Butterfly.Streaming.InMemory
{
    public class InMemorySpanConsumerCallback : ISpanConsumerCallback
    {
        private readonly ISpanStorage _spanStorage;

        public InMemorySpanConsumerCallback(ISpanStorage spanStorage)
        {
            _spanStorage = spanStorage;
        }

        public Task InvokeAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            return _spanStorage.StoreAsync(spans, cancellationToken);
        }
    }
}