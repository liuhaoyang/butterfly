using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Microsoft.Extensions.Logging;

namespace Butterfly.Pipeline.Lite
{
    internal class ServicePipelineTarget : IPipelineTarget
    {
        private readonly ILogger _logger;
        private readonly IServiceStorage _serviceStorage;
        private readonly IPipelineSource<IEnumerable<Span>> _streamingSource;
        private ActionBlock<IEnumerable<Service>> _consumer;

        public ServicePipelineTarget(IPipelineSource<IEnumerable<Span>> streamingSource, IServiceStorage serviceStorage, ILogger<SpanPipelineTarget> logger)
        {
            _logger = logger;
            _streamingSource = streamingSource;
            _serviceStorage = serviceStorage;
        }

        public async Task Complete()
        {
            await _consumer.Completion;
        }

        public Task Executing()
        {
            var transformt = new TransformBlock<IEnumerable<Span>, IEnumerable<Service>>(spans => spans.Select(x => new Service { Name = ServiceUtils.GetService(x) }).ToArray());
            _consumer = new ActionBlock<IEnumerable<Service>>(async services => await ConsumerAction(services));
            _streamingSource.SourceBlock.LinkTo(transformt);
            transformt.LinkTo(_consumer);
            return TaskUtils.CompletedTask;
        }

        private async Task ConsumerAction(IEnumerable<Service> services)
        {
            try
            {
                await _serviceStorage.StoreServiceAsync(services.Where(x => x.Name != null), CancellationToken.None);
            }
            catch (Exception exception)
            {
                _logger.LogError("Store services error.", exception);
                throw;
            }
        }
    }
}