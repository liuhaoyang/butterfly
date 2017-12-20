using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Flow
{
    public interface ISpanConsumer
    {
        Task AcceptAsync(ISpanConsumerCallback callback, CancellationToken cancellationToken);
    }
}