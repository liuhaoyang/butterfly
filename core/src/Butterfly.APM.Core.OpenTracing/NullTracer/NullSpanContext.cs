using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.APM.Core.OpenTracing.NullTracer
{
    public class NullSpanContext : ISpanContext
    {
        public string TraceId => string.Empty;

        public string SpanId => string.Empty;

        public bool Sampled => true;

        public Baggage Baggage { get; } = new Baggage();
    }
}