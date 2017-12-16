using System;

namespace Butterfly.OpenTracing
{
    internal class SpanContext : ISpanContext
    {
        public string TraceId { get; }

        public string SpanId { get; }

        public bool Sampled { get; }

        public Baggage Baggage { get; }

        public SpanContext(string traceId, string spanId, bool sampled, Baggage baggage)
        {
            TraceId = traceId;
            SpanId = spanId;
            Sampled = sampled;
            Baggage = baggage ?? throw new ArgumentNullException(nameof(baggage));
        }
    }
}