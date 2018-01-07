using System.Linq;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage.Query;

namespace Butterfly.Server.Common
{
    public static class ServiceHelpers
    {
        public const string UnKnownService = "unknown";
        
        public static string GetService(Span span)
        {
            return span?.Tags?.FirstOrDefault(x => x.Key == QueryConstants.Service)?.Value ?? UnKnownService;
        }
    }
}