using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Core
{
    public interface ICollector
    {
        Task Start(CancellationToken cancellationToken);

        Task Stop(CancellationToken cancellationToken);
    }
}