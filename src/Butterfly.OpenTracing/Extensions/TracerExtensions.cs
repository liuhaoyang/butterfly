namespace Butterfly.OpenTracing.Extensions
{
    public static class TracerExtensions
    {
        public static ISpan GetCurrentSpan(this ITracer tracer)
        {
            return SpanLocal.Current;
        }

        public static void SetCurrentSpan(this ITracer tracer, ISpan spanContext)
        {
            SpanLocal.Current = spanContext;
        }
    }
}