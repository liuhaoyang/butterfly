using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;

namespace Butterfly.EntityFrameworkCore
{
    public class InMemoryServiceStorage : IServiceStorage
    {
        public Task StoreServiceAsync(IEnumerable<Service> services, CancellationToken cancellationToken)
        {
            return TaskUtils.CompletedTask;
        }
    }
}