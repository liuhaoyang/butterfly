using Butterfly.Common;
using Microsoft.Extensions.Configuration;

namespace Butterfly.Streaming.InMemory
{
    public static class ConfigurationExtensions
    {
        public static bool EnableInMemoryStreaming(this IConfiguration configuration)
        {
            var streamType = configuration[EnvironmentUtils.StreamType];
            if (streamType == null)
                return true;
            return streamType.ToLower() == EnvironmentUtils.InMemory;
        }
    }
}