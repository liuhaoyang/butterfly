using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Storage
{
    public interface ISpanStorage
    {
        Task StoreAsync(IEnumerable<Span> spans, CancellationToken cancellationToken);

        Task<IEnumerable<Span>> GetAll();
    }
}