using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Consumer.Lite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddOptions();
            services.Configure<ConsumerOptions>(configuration);
            services.AddSingleton(typeof(ISource<>), typeof(BroadcastSource<>));
            services.AddSingleton<IConsumer, SpanConsumer>();
            services.AddSingleton<IConsumer, ServiceConsumer>();
            services.AddSingleton<ISpanProducer, InMemorySpanProducer>();
            services.AddSingleton<IHostedService, ConsumerService>();

            return services;
        }
    }
}