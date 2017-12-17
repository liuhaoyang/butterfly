using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Storage;
using Butterfly.Protocol;

namespace Butterfly.EntityFrameworkCore
{
    public class EFCoreSpanStorage: ISpanStorage
    {
        public Task StoreAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}