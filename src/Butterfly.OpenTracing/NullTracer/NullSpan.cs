using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.OpenTracing.NullTracer
{
    public class NullSpan : ISpan
    {
        public long StartTimestamp => 0;

        public long FinishTimestamp => 0;

        public long Duration => 0;

        public ISpanContext SpanContext { get; } = new NullSpanContext();

        public Baggage Baggage => SpanContext.Baggage;

        public TagCollection Tags { get; } = new TagCollection();

        public string OperationName => string.Empty;

        public void Dispose()
        {
            Finish();
        }

        public void Finish()
        {
        }
    }
}