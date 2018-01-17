using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Butterfly.DataContract.Tracing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Nest;

namespace Butterfly.Elasticsearch
{
    internal class IndexManager : IIndexManager
    {
        private const string IndexSuffix = "butterfly";

        private readonly IMemoryCache _memoryCache;
        private readonly ElasticClient _elasticClient;
        private ILogger _logger;

        public IndexManager(IMemoryCache memoryCache, IElasticClientFactory elasticClientFactory, ILogger<IndexManager> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _elasticClient = elasticClientFactory.Create();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IndexName CreateIndex(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset == null)
            {
                return $"{IndexSuffix}-*";
            }

            var index = $"{IndexSuffix}-{dateTimeOffset.Value:yyyyMMdd}";
            if (_memoryCache.TryGetValue(index, out _))
            {
                return index;
            }

            var existsResponse = _elasticClient.IndexExists(Indices.Index(index));
            if (!existsResponse.Exists)
            {
                CreateIndexExecute(index);
            }

            _memoryCache.Set<bool>(index, true, TimeSpan.FromHours(6));
            return index;
        }

        private void CreateIndexExecute(string index)
        {
            _logger.LogInformation($"Not exists index {index}.");
            var tracingIndex = new CreateIndexDescriptor(index);
            tracingIndex.Mappings(x => x.Map<Span>(m => m.AutoMap().Properties(p => p.Nested<Tag>(n => n.Name(name => name.Tags).AutoMap()))));
            var response = _elasticClient.CreateIndex(tracingIndex);
            if (response.IsValid)
            {
                _logger.LogInformation($"Create index {index} success.");
            }
            else
            {
                var exception = new InvalidOperationException($"Create index {index} error : {response.ServerError}");
                _logger.LogError(exception, exception.Message);
                throw exception;
            }
        }
    }
}