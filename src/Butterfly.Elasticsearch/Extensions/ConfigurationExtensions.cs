using System;
using System.Collections.Generic;
using System.Text;
using Butterfly.Common;
using Microsoft.Extensions.Configuration;

namespace Butterfly.Elasticsearch
{
    internal static class ConfigurationExtensions
    {
        public static bool EnableElasticsearchStorage(this IConfiguration configuration)
        {
            var storageType = configuration[EnvironmentUtils.StorageType];
            return storageType.ToLower() == EnvironmentUtils.Elasticsearch;
        }
    }
}
