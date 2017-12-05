using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Butterfly.Core.Server
{
    public interface IServer
    {
        Task Start(CancellationToken cancellationToken);
    }
}