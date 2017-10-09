using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.APM.Core.OpenTracing
{
    internal class SpanContext : ISpanContext
    {
        private readonly ReadOnlyBaggage _readOnlyBaggage;

        public string TraceId { get; }

        public string SpanId { get; }

        public bool Sampled => throw new NotImplementedException();

        public SpanContext(string traceId, string spanId, bool sampled, ReadOnlyBaggage readOnlyBaggage)
        {
            TraceId = traceId;
            SpanId = spanId;
        }

        public ReadOnlyBaggage GetBaggage()
        {
            return _readOnlyBaggage;
        }
    }
}