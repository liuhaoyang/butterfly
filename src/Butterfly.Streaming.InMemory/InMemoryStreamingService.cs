using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Butterfly.Streaming.InMemory
{
    public class InMemoryStreamingService : IStreamingService
    {
        private readonly IStreamingSource<IEnumerable<Span>> _streamingSource;
        private readonly IEnumerable<IStreamingTarget> _streamingTargets;

        public InMemoryStreamingService(IStreamingSource<IEnumerable<Span>> streamingSource, IEnumerable<IStreamingTarget> streamingTargets)
        {
            _streamingSource = streamingSource;
            _streamingTargets = streamingTargets;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var target in _streamingTargets)
                await target.Executing();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _streamingSource.Complete();
            foreach (var target in _streamingTargets)
                await target.Complete();
        }
    }
}