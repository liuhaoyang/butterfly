using System.Collections.Generic;
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