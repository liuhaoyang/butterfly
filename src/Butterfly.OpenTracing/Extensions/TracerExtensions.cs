using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.OpenTracing.Extensions
{
    public static class TracerExtensions
    {
        public static ISpanContext GetLocalSpanContext(this ITracer tracer)
        {
            throw new NotImplementedException();
        }

        public static void SetLocalSpanContext(this ITracer tracer, ISpanContext spanContext)
        {
            throw new NotImplementedException();
        }
    }
}