using Microsoft.Extensions.Configuration;

namespace Butterfly.Server.Common
{
    internal static class UrlHelpers
    {
        private const string defaultUrl = "http://localhost:9618";
        private const string UrlKey = "ASPNETCORE_URLS";

        internal static string GetApplicationUrl()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, false)
                .AddEnvironmentVariables()
                .Build();
            
            return configuration[UrlKey] ?? defaultUrl;
        }
    }
}