using Butterfly.Common;
using Microsoft.Extensions.Configuration;

namespace Butterfly.EntityFrameworkCore
{
    internal static class ConfigurationExtensions
    {
        public static bool EnableInMemoryStorage(this IConfiguration configuration)
        {
            var storageType = configuration[EnvironmentUtils.StorageType];
            if (storageType == null)
                return true;
            return storageType.ToLower() == EnvironmentUtils.InMemory;
        }
    }
}