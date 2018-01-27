using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Streaming
{
    public interface ISpanConsumer
    {
        Task AcceptAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}