using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.Configuration;

namespace Butterfly.Server.Common
{
    internal static class UrlHelpers
    {
        private const string defaultUrl = "http://localhost:9618";
        private const string UrlKey = "server:address";

        internal static string GetApplicationUrl(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false)
                .AddJsonFile("appsettings.Development.json", true, false)
                .AddJsonFile("appsettings.Production.json", true, false)
                .AddEnvironmentVariables();

            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }
            
            var configuration = configurationBuilder.Build();           
            return configuration[UrlKey] ?? defaultUrl;
        }
    }
}