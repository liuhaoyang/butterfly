using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Core
{
    public interface IHostedCollector
    {
        Task Start(CancellationToken cancellationToken);

        Task Stop(CancellationToken cancellationToken);
    }
}