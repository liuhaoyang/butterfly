using Butterfly.Common;
using Microsoft.Extensions.Configuration;

namespace Butterfly.Flow.InMemory
{
    public static class ConfigurationExtensions
    {
        public static bool EnableInMemoryFlow(this IConfiguration configuration)
        {
            var flowType = configuration[EnvironmentUtils.FlowType];
            if (flowType == null)
                return true;
            return flowType.ToLower() == EnvironmentUtils.InMemory;
        }
    }
}