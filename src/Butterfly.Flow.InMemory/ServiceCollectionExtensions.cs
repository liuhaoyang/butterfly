using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Flow.InMemory
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

            if (configuration.EnableInMemoryFlow())
            {
                services.AddOptions();
                services.Configure<InMemoryFlowOptions>(configuration);
                services.AddScoped<ISpanConsumerCallback, InMemorySpanConsumerCallback>();
                services.AddSingleton(typeof(IBlockingQueue<>), typeof(BlockingQueue<>));
                services.AddSingleton<ISpanConsumer, InMemorySpanConsumer>();
                services.AddSingleton<ISpanProducer, InMemorySpanProducer>();
                services.AddSingleton<IFlowService, InMemoryFlowService>();
                services.AddSingleton<IHostedService>(provider => provider.GetService<IFlowService>());
            }

            return services;
        }
    }
}