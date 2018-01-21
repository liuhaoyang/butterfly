using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Butterfly.Streaming.InMemory
{
    internal class SpanStreamingTarget : IStreamingTarget
    {
        private const int DEFAUKT_CONSUMER_PARALLELISM = 2;
        private readonly IStreamingSource<IEnumerable<Span>> _streamingSource;
        private readonly InMemoryStreamingOptions _options;
        private readonly IServiceProvider _serviceProvider;
        private readonly ValueTuple<ActionBlock<IEnumerable<Span>>, IServiceScope> _consumers;
        private readonly ILogger _logger;

        public SpanStreamingTarget(IStreamingSource<IEnumerable<Span>> streamingSource, IServiceProvider serviceProvider, IOptions<InMemoryStreamingOptions> options, ILogger<SpanStreamingTarget> logger)
        {
            _streamingSource = streamingSource;
            _options = options.Value;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _consumers = new ValueTuple<ActionBlock<IEnumerable<Span>>, IServiceScope>();
        }

        public Task Executing()
        {
            var (targetBlock, serviceScope) = CreateConsumer();
            _streamingSource.SourceBlock.LinkTo(targetBlock);
            return TaskUtils.CompletedTask;
        }

        private (ActionBlock<IEnumerable<Span>>, IServiceScope) CreateConsumer()
        {
            var serviceScope = _serviceProvider.CreateScope();
            var spanStorage = serviceScope.ServiceProvider.GetRequiredService<ISpanStorage>();
            if (_options.ConsumerCapacity <= 0)
                return (new ActionBlock<IEnumerable<Span>>(async data => await ConsumerAction(spanStorage, data)), serviceScope);
            var executionDataflowBlockOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = _options.ConsumerCapacity,
                MaxDegreeOfParallelism = _options.MaxConsumerParallelism <= 0 ? DEFAUKT_CONSUMER_PARALLELISM : _options.MaxConsumerParallelism
            };
            return (new ActionBlock<IEnumerable<Span>>(async data => await ConsumerAction(spanStorage, data), executionDataflowBlockOptions), serviceScope);
        }

        private async Task ConsumerAction(ISpanStorage spanStorage, IEnumerable<Span> spans)
        {
            try
            {
                await spanStorage.StoreAsync(spans);
            }
            catch (Exception exception)
            {
                _logger.LogError("Store spans error.", exception);
                throw;
            }
        }

        public async Task Complete()
        {
            var (targetBlock, serviceScope) = _consumers;
            await targetBlock.Completion;
            serviceScope.Dispose();
        }
    }
}