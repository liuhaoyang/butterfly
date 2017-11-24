using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.OpenTracing
{
    public static class SpanContextExtensions
    {
        public static SpanContextPackage Package(this ISpanContext spanContext)
        {
            return new SpanContextPackage(spanContext.TraceId, spanContext.SpanId, spanContext.Sampled, spanContext.Baggage);
        }
    }
}
