using System;
using System.Threading;

namespace Butterfly.Core
{
    public class ManualResetEvent : IResetEvent
    {
        private readonly ManualResetEventSlim _eventSlim;

        public ManualResetEvent()
        {
            _eventSlim = new ManualResetEventSlim(false, 1);
        }

        public void Dispose()
        {
            _eventSlim.Dispose();
        }

        public bool IsSet => _eventSlim.IsSet;

        public void Reset()
        {
            _eventSlim.Reset();
        }

        public void Set()
        {
            _eventSlim.Set();
        }

        public void Wait(CancellationToken cancellationToken)
        {
            _eventSlim.Wait(cancellationToken);
        }

        public void Wait(TimeSpan timeout, CancellationToken cancellationToken)
        {
            _eventSlim.Wait(timeout, cancellationToken);
        }
    }
}