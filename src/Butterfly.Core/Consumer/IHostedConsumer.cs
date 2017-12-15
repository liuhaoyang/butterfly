using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Core.Consumer
{
    public interface IHostedConsumer
    {
        Task Start(CancellationToken cancellationToken);

        Task Stop(CancellationToken cancellationToken);
    }
}