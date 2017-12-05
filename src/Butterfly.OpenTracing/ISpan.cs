using System;

namespace Butterfly.OpenTracing
{
    public interface ISpan : IDisposable
    {
        long StartTimestamp { get; }

        long FinishTimestamp { get; }

        long Duration { get; }

        string OperationName { get; }

        ISpanContext SpanContext { get; }

        Baggage Baggage { get; }

        TagCollection Tags { get; }

        void Finish();
    }
}