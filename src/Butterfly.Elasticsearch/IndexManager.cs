using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Butterfly.DataContract.Tracing;
using Microsoft.Extensions.Caching.Memory;
using Nest;

namespace Butterfly.Elasticsearch
{
    internal class IndexManager : IIndexManager
    {
        private const string IndexSuffix = "butterfly";

        private readonly IMemoryCache _memoryCache;
        private readonly ElasticClient _elasticClient;

        public IndexManager(IMemoryCache memoryCache, IElasticClientFactory elasticClientFactory)
        {
            _memoryCache = memoryCache;
            _elasticClient = elasticClientFactory.Create();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IndexName GetIndex(DateTimeOffset dateTimeOffset)
        {
            var index = $"{IndexSuffix}-{dateTimeOffset:yyyyMMdd}";
            if (_memoryCache.TryGetValue(index, out _))
            {
                return index;
            }
            var existsResponse = _elasticClient.IndexExists(Indices.Index(index));
            if (!existsResponse.Exists)
            {
                var tracingIndex = new CreateIndexDescriptor(index);
                tracingIndex.Mappings(x => x.Map<Span>(m => m.AutoMap()));
                var response = _elasticClient.CreateIndex(tracingIndex);
                //todo log response
            }
            _memoryCache.Set<bool>(index, true, TimeSpan.FromMinutes(30));
            return index;
        }
    }
}