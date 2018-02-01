using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Pipeline
{
    public interface ISpanConsumer
    {
        Task AcceptAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}