using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Flow
{
    public interface IFlowService
    {
        Task Start(CancellationToken cancellationToken);

        Task Stop(CancellationToken cancellationToken);
    }
}