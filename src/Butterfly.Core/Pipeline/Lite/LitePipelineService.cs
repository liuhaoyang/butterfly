using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Pipeline.Lite
{
    public class LitePipelineService : IHostedService
    {
        private readonly IPipelineSource<IEnumerable<Span>> _streamingSource;
        private readonly IEnumerable<IPipelineTarget> _streamingTargets;

        public LitePipelineService(IPipelineSource<IEnumerable<Span>> streamingSource, IEnumerable<IPipelineTarget> streamingTargets)
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