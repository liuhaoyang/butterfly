using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.OpenTracing.NullTracer
{
    public class NullSpan : ISpan
    {
        public ISpanContext SpanContext { get; } = new NullSpanContext();

        public Baggage Baggage => SpanContext.Baggage;

        public TagCollection Tags { get; } = new TagCollection();
        
        public LogCollection Logs { get; } =new LogCollection();
        
        public void Finish(DateTimeOffset finishTimestamp)
        {
        }

        public DateTimeOffset StartTimestamp { get; set; }
        
        public DateTimeOffset FinishTimestamp { get; set; }
        
        public string OperationName => string.Empty;

        public void Dispose()
        {
            Finish(DateTimeOffset.UtcNow);
        }
    }
}