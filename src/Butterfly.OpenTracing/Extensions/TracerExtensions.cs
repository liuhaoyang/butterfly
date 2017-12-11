using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.OpenTracing.Extensions
{
    public static class TracerExtensions
    {
        public static ISpanContext GetLocalSpanContext(this ITracer tracer)
        {
            return SpanContextLocal.Current;
        }

        public static void SetLocalSpanContext(this ITracer tracer, ISpanContext spanContext)
        {
            SpanContextLocal.Current = spanContext;
        }
    }
}