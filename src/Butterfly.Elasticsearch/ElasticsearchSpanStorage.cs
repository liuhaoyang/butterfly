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

        public ElasticsearchSpanStorage(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task StoreAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            if (spans == null)
            {
                return TaskUtils.FailCompletedTask;
            }

            var operations = spans.Select(x => new BulkIndexOperation<Span>(x)).ToArray();
            var bulkRequest = new BulkRequest();

        }
    }
}
