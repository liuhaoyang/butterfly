#if NET45
using System.Runtime.Remoting.Messaging;
#else
using System.Threading;
#endif


namespace Butterfly.OpenTracing
{
    internal static class SpanContextLocal
    {
#if NET45 || NET451 || NET452
        private const string SpanContextKey = "butterfly_spancontext";
#else
        private static readonly AsyncLocal<ISpanContext> AsyncLocal = new AsyncLocal<ISpanContext>();
#endif

        public static ISpanContext Current
        {
#if NET45 || NET451 || NET452
            get
            {
                return CallContext.LogicalGetData(SpanContextKey) as ISpanContext;
            }
            set
            {
                CallContext.LogicalSetData(SpanContextKey, value);
            }
#else
            get
            {
                return AsyncLocal.Value;
            }
            set
            {
                AsyncLocal.Value = value;
            }
#endif
        }
    }
}