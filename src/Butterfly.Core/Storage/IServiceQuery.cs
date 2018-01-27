using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage.Query;

namespace Butterfly.Storage
{
    public interface IServiceQuery
    {
        Task<IEnumerable<Service>> GetServices(TimeRangeQuery query);
    }
}