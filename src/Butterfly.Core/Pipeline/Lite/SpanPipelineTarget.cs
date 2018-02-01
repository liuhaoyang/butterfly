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

namespace Butterfly.Pipeline.Lite
{
    internal class SpanPipelineTarget : IPipelineTarget
    {
        private const int DEFAUKT_CONSUMER_PARALLELISM = 2;
        private readonly IPipelineSource<IEnumerable<Span>> _streamingSource;
        private readonly LitePipelineOptions _options;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private ActionBlock<IEnumerable<Span>> _consumer;

        public SpanPipelineTarget(IPipelineSource<IEnumerable<Span>> streamingSource, IServiceProvider serviceProvider, IOptions<LitePipelineOptions> options, ILogger<SpanPipelineTarget> logger)
        {
            _streamingSource = streamingSource;
            _options = options.Value;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task Executing()
        {
            _consumer = CreateConsumer();
            _streamingSource.SourceBlock.LinkTo(_consumer);
            return TaskUtils.CompletedTask;
        }

        private ActionBlock<IEnumerable<Span>> CreateConsumer()
        {
            var executionDataflowBlockOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = _options.ConsumerCapacity <= 0 ? -1 : _options.ConsumerCapacity,
                MaxDegreeOfParallelism = _options.MaxConsumerParallelism <= 0 ? DEFAUKT_CONSUMER_PARALLELISM : _options.MaxConsumerParallelism
            };
            return new ActionBlock<IEnumerable<Span>>(async data => await ConsumerAction(data), executionDataflowBlockOptions);
        }

        private async Task ConsumerAction(IEnumerable<Span> spans)
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var spanStorage = serviceScope.ServiceProvider.GetRequiredService<ISpanStorage>();
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
        }

        public async Task Complete()
        {
            await _consumer?.Completion;
        }
    }
}