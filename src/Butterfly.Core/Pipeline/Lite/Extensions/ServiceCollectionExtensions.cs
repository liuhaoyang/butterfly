using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace Butterfly.Pipeline.Lite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLitePipeline(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration.EnableLitePipeline())
            {
                services.AddOptions();
                services.Configure<LitePipelineOptions>(configuration);
                services.AddSingleton(typeof(IPipelineSource<>), typeof(PipelineSource<>));
                services.AddSingleton<IPipelineTarget, SpanPipelineTarget>();
                services.AddSingleton<IPipelineTarget, ServicePipelineTarget>();
                services.AddSingleton<ISpanProducer, InMemorySpanProducer>();
                services.AddSingleton<IHostedService, LitePipelineService>();
            }

            return services;
        }
    }
}