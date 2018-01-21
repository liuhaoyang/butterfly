using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract;

namespace Butterfly.Streaming
{
    public interface ISpanConsumer
    {
        Task AcceptAsync(ISpanConsumerCallback callback, CancellationToken cancellationToken);
    }
}