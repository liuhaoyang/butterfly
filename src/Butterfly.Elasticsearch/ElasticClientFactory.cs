using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Butterfly.DataContract.Tracing;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;

namespace Butterfly.Elasticsearch
{
    public class ElasticClientFactory : IElasticClientFactory
    {
        private readonly ElasticsearchOptions _elasticsearchOptions;
        private readonly Lazy<ElasticClient> _value;
        private readonly ILogger _logger;

        public ElasticClientFactory(IOptions<ElasticsearchOptions> options, ILogger<ElasticClientFactory> logger)
        {
            _logger = logger;
            _elasticsearchOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _value = new Lazy<ElasticClient>(CreatElasticClient, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public ElasticClient Create()
        {
            return _value.Value;
        }

        private ElasticClient CreatElasticClient()
        {
            try
            {
                if (string.IsNullOrEmpty(_elasticsearchOptions.ElasticsearchUrls))
                {
                    throw new InvalidOperationException("Invalid ElasticsearchUrls.");
                }
                _logger.LogInformation($"Using elasticsearch connection pool with {_elasticsearchOptions.ElasticsearchUrls}.");
                var urls = _elasticsearchOptions.ElasticsearchUrls.Split(';').Select(x => new Uri(x)).ToArray();
                var pool = new StaticConnectionPool(urls);
                var settings = new ConnectionSettings(pool);
                var client = new ElasticClient(settings);       
                return client;
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Create ElasticClient failed.");
                throw;
            }
           
        }
    }
}