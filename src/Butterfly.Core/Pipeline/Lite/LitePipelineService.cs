using System.Collections.Generic;
using System.Linq;
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
            await Task.WhenAll(_streamingTargets.Select(x => x.Executing()));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _streamingSource.Complete();

            await Task.WhenAll(_streamingTargets.Select(x => x.Complete()));
        }
    }
}