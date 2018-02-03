using Butterfly.Common;
using Microsoft.Extensions.Configuration;

namespace Butterfly.Pipeline.Lite
{
    public static class ConfigurationExtensions
    {
        public static bool EnableLitePipeline(this IConfiguration configuration)
        {
            var streamType = configuration[EnvironmentUtils.Analyzer];
            if (streamType == null)
                return true;
            return streamType.ToLower() == EnvironmentUtils.InMemory;
        }
    }
}