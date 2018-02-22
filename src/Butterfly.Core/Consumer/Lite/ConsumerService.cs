using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Consumer.Lite
{
    public class ConsumerService : IHostedService
    {
        private readonly ISource<IEnumerable<Span>> _streamingSource;
        private readonly IEnumerable<IConsumer> _streamingTargets;

        public ConsumerService(ISource<IEnumerable<Span>> streamingSource, IEnumerable<IConsumer> streamingTargets)
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