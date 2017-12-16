using System;
using System.Threading;

namespace Butterfly.Core
{
    public interface IResetEvent : IDisposable
    {
        bool IsSet { get; }

        void Reset();

        void Set();

        void Wait(CancellationToken cancellationToken);

        void Wait(TimeSpan timeout, CancellationToken cancellationToken);
    }
}