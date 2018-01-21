using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Streaming.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryFlow(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.EnableInMemoryStreaming())
            {
                services.AddOptions();
                services.Configure<InMemoryStreamingOptions>(configuration);
                services.AddScoped<ISpanConsumerCallback, InMemorySpanConsumerCallback>();
                services.AddSingleton(typeof(IBlockingQueue<>), typeof(BlockingQueue<>));
                services.AddSingleton<ISpanConsumer, InMemorySpanConsumer>();
                services.AddSingleton<ISpanProducer, InMemorySpanProducer>();
                services.AddSingleton<IStreamingService, InMemoryStreamingService>();
                services.AddSingleton<IHostedService>(provider => provider.GetService<IStreamingService>());
            }

            return services;
        }
    }
}