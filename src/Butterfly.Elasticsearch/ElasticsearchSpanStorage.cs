using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Nest;

namespace Butterfly.Elasticsearch
{
    public class ElasticsearchSpanStorage : ISpanStorage
    {
        private readonly ElasticClient _elasticClient;
        private readonly IIndexFactory _indexFactory;

        public ElasticsearchSpanStorage(IElasticClientFactory elasticClientFactory, IIndexFactory indexFactory)
        {
            _elasticClient = elasticClientFactory?.Create() ?? throw new ArgumentNullException(nameof(elasticClientFactory));
            _indexFactory = indexFactory ?? throw new ArgumentNullException(nameof(indexFactory));
        }

        public Task StoreAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            if (spans == null)
            {
                return TaskUtils.FailCompletedTask;
            }

            var bulkRequest = new BulkRequest {Operations = new List<IBulkOperation>()};
            foreach (var span in spans)
            {
                var operation = new BulkIndexOperation<Span>(span) {Index = _indexFactory.CreateIndex()};
                bulkRequest.Operations.Add(operation);
            }

            return _elasticClient.BulkAsync(bulkRequest, cancellationToken);
        }
    }
}