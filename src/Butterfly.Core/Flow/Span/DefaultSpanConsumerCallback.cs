using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;

namespace Butterfly.Flow
{
    public class DefaultSpanConsumerCallback : ISpanConsumerCallback
    {
        private static readonly ISpanStorage[] nullSpanStorages = new ISpanStorage[0];

        private readonly ISpanStorage _spanStorage;

        public DefaultSpanConsumerCallback(ISpanStorage spanStorage)
        {
            _spanStorage = spanStorage ?? throw new ArgumentNullException(nameof(spanStorage));
        }

        public async Task InvokeAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            await _spanStorage.StoreAsync(spans, cancellationToken);
        }
    }
}