using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;

namespace Butterfly.Storage
{
    public interface IServiceStorage
    {
        Task StoreServiceAsync(IEnumerable<Service> services, CancellationToken cancellationToken);
    }
}
