using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Nest;

namespace Butterfly.Elasticsearch
{
    internal class ElasticSearchServiceStorage : IServiceStorage
    {
        private readonly ElasticClient _elasticClient;
        private readonly IIndexManager _indexManager;

        public ElasticSearchServiceStorage(IElasticClientFactory elasticClientFactory, IIndexManager indexManager)
        {
            _elasticClient = elasticClientFactory?.Create() ?? throw new ArgumentNullException(nameof(elasticClientFactory));
            _indexManager = indexManager ?? throw new ArgumentNullException(nameof(indexManager));
        }

        public Task StoreServiceAsync(IEnumerable<Service> services, CancellationToken cancellationToken)
        {
            if (services == null)
            {
                return TaskUtils.FailCompletedTask;
            }

            var bulkRequest = new BulkRequest { Operations = new List<IBulkOperation>() };

            foreach (var service in services)
            {
                var operation = new BulkIndexOperation<Service>(service) { Index = _indexManager.CreateTracingIndex(DateTimeOffset.UtcNow) };
                bulkRequest.Operations.Add(operation);
            }

            return _elasticClient.BulkAsync(bulkRequest, cancellationToken);
        }
    }
}