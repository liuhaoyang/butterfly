using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanQueue
    {
        void Enqueue(ISpan span);

        bool TryDequeue(out ISpan span);
    }
}