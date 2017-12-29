using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Butterfly.Flow.InMemory
{
    public class InMemorySpanConsumerCallback :  ISpanConsumerCallback
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemorySpanConsumerCallback(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task InvokeAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var spanStorage = scope.ServiceProvider.GetRequiredService<ISpanStorage>();
                return spanStorage.StoreAsync(spans, cancellationToken);
            }
        }
    }
}