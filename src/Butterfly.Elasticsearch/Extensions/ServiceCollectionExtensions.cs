using System;
using System.Collections.Generic;
using System.Text;
using Butterfly.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Butterfly.Elasticsearch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticsearchStorage(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.EnableElasticsearchStorage())
            {
                services.AddOptions();
                services.Configure<ElasticsearchOptions>(configuration);
                services.AddSingleton<IIndexManager, IndexManager>();
                services.AddSingleton<IElasticClientFactory, ElasticClientFactory>();
                services.AddScoped<ISpanStorage, ElasticsearchSpanStorage>();
                services.AddScoped<ISpanQuery, ElasticsearchSpanQuery>();
                services.AddScoped<IServiceQuery, ElasticSearchServiceQuery>();
                services.AddSingleton<IServiceStorage, ElasticSearchServiceStorage>();
            }

            return services;
        }
    }
}
