namespace Butterfly.APM.OpenTracing
{
    public struct SpanContextPackage
    {
        public string TraceId { get; }

        public string SpanId { get; }

        public Baggage Baggage { get; }

        public bool Sampled { get; }

        public SpanContextPackage(string traceId, string spanId, bool sampled, Baggage baggage)
        {
            TraceId = traceId;
            SpanId = spanId;
            Sampled = sampled;
            Baggage = baggage;
        }
    }
}