using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Consumer
{
    public interface ISpanConsumer
    {
        Task AcceptAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}