using System;

namespace Butterfly.APM.OpenTracing
{
    public interface ISpanContext
    {
        string TraceId { get; }

        string SpanId { get; }

        bool Sampled { get; }

        Baggage Baggage { get; }
    }
}