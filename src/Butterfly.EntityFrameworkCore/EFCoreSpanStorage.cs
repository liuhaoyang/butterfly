using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Core;
using Butterfly.Protocol;

namespace Butterfly.EntityFrameworkCore
{
    public class EFCoreSpanStorage: ISpanStorage
    {
        public Task Store(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}