using Butterfly.Server.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Butterfly.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args, UrlHelpers.GetApplicationUrl()).Run();
        }

        public static IWebHost BuildWebHost(string[] args, string url) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();

    }
}