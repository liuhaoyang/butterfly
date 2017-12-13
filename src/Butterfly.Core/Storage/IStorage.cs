using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.Protocol;

namespace Butterfly.Core.Storage
{
    public interface IStorage
    {
        Task Accept(IEnumerable<Span> spans);
    }
}