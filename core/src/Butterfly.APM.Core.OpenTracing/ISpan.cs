using System;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpan : IDisposable
    {
        long StartTimestamp { get; }

        long FinishTimestamp { get; }

        long ElapsedTicks { get; }

        ISpanContext SpanContext { get; }

        Baggage Baggage { get; }

        TagCollection Tags { get; }

        void Finish();
    }
}