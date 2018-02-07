using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;

// ReSharper disable once CheckNamespace
namespace Butterfly.Pipeline.Lite
{
    public class InMemorySpanProducer : ISpanProducer
    {
        private readonly IPipelineSource<IEnumerable<Span>> _streamingSource;

        public InMemorySpanProducer(IPipelineSource<IEnumerable<Span>> streamingSource)
        {
            _streamingSource = streamingSource ?? throw new ArgumentNullException(nameof(streamingSource));
        }

        public Task PostAsync(IEnumerable<Span> spans, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (spans == null)
            {
                throw new ArgumentNullException(nameof(spans));
            }

            _streamingSource.Post(spans);
            return TaskUtils.CompletedTask;
        }
    }
}