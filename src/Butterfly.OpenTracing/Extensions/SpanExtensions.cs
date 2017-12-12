using System;

namespace Butterfly.OpenTracing
{
    public static class SpanExtensions
    {  
        public static ISpan SetBaggage(this ISpan span, string key, string value)
        {
            if (span == null)
            {
                throw new ArgumentNullException(nameof(span));
            }
            span.SpanContext.SetBaggage(key, value);
            return span;
        }

        public static ISpan Log(this ISpan span, DateTime timestamp, LogField fields)
        {
            if (span == null)
            {
                throw new ArgumentNullException(nameof(span));
            }
            span.Logs.Add(new LogData(timestamp, fields));
            return span;
        }

        public static ISpan Log(this ISpan span, LogField fields)
        {
            return Log(span, DateTime.UtcNow, fields);
        }
    }
}