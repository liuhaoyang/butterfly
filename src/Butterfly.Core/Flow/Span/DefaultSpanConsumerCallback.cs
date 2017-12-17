using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;
using Butterfly.Storage;

namespace Butterfly.Flow
{
    public class DefaultSpanConsumerCallback : ISpanConsumerCallback
    {
        private static readonly ISpanStorage[] nullSpanStorages = new ISpanStorage[0];

        private readonly IEnumerable<ISpanStorage> _spanStorages;

        public DefaultSpanConsumerCallback(IEnumerable<ISpanStorage> spanStorages)
        {
            _spanStorages = spanStorages ?? nullSpanStorages;
        }

        public async Task InvokeAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            foreach (var spanStorage in _spanStorages)
            {
                await spanStorage.StoreAsync(spans, cancellationToken);
            }
        }
    }
}