using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Butterfly.Collector
{
    public interface IHostedCollector : IHostedService
    {
    }
}